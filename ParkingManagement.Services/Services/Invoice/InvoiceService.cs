using ParkingManagement.CommonLibrary;
using ParkingManagement.Providers.VehicleStaysProvider;
using ParkingManagement.Services.DataModels;
using ParkingManagement.Services.Providers.ParkingRatesProvider;
using ParkingManagement.Services.Providers.VehiclesProvider;
using ParkingManagement.Services.Services.Invoice.Models;

namespace ParkingManagement.Services.Services.Invoice
{
    internal class InvoiceService : IInvoiceService
    {
        private readonly IParkingRatesProvider parkingRatesProvider;
        private readonly IVehiclesProvider vehicleProvider;
        private readonly IVehicleStaysProvider vehicleStaysProvider;

        public InvoiceService(IParkingRatesProvider parkingRatesProvider,
            IVehiclesProvider vehicleProvider,
            IVehicleStaysProvider vehicleStaysProvider)
        {
            this.parkingRatesProvider = parkingRatesProvider;
            this.vehicleProvider = vehicleProvider;
            this.vehicleStaysProvider = vehicleStaysProvider;
        }

        public Task<DetailedStayInvoice> GenerateDetailedInvoiceAsync()
        {
            throw new NotImplementedException();
        }

        public StayInvoice GenerateInvoice(InvoiceRequestData requestData)
        {
            var (totalMinutes, totalAmountToPay) = GetInvoiceDetails(requestData);

            return new StayInvoice()
            {
                LicensePlate = requestData.LicensePlate,
                TotalTimeInMinutes = totalMinutes,
                TotalAmountToPay = totalAmountToPay
            };
        }

        public async Task<IEnumerable<StayInvoice>> GenerateOverallInvoicesByVehicleTypeAsync(VehicleType vehicleType)
        {
            var licensePlatesByVehicleType = vehicleProvider.GetAllLicensePlatesByVehicleType(vehicleType).ToList();

            var stays = await GetStays(licensePlatesByVehicleType);

            stays = stays.Select(stay =>
            {
                if (!stay.StayCompleted)
                {
                    stay.ExitTime = DateTime.UtcNow;
                }
                return stay;
            });

            return GenerateOverallInvoices(stays, vehicleType, true);
        }

        private async Task<IEnumerable<VehicleStayDto>> GetStays(IEnumerable<string> licensePlates)
        {
            var stays = await vehicleStaysProvider.GetAllAsync();
            return stays.Where(stay => licensePlates.Contains(stay.LicensePlate));
        }

        private IEnumerable<StayInvoice> GenerateOverallInvoices(IEnumerable<VehicleStayDto> vehicleStays, VehicleType vehicleType, bool calculateAmountToPay)
        {
            var groups = vehicleStays.GroupBy(stay => stay.LicensePlate);

            return groups.Select(group => GenerateInvoice(new InvoiceRequestData()
            {
                LicensePlate = group.Key,
                VehicleType = vehicleType,
                CalculateAmountToPay = calculateAmountToPay,
                StaysTimeRanges = group.Select(stay =>
                {
                    if (stay.ExitTime is null)
                    {
                        throw new Exception("Exit Time should not be Null when generating Overall Invoices");
                    }

                    return new VehicleStayTimeRange()
                    {
                        EntryTime = stay.EntryTime,
                        ExitTime = (DateTime)stay.ExitTime
                    };
                })
            }));
        }

        private (int TotalMinutesInParking, double TotalAmountToPay) GetInvoiceDetails(InvoiceRequestData requestData)
        {
            var totalMinutes = GetTotalMinutesInParking(requestData.StaysTimeRanges);

            if (requestData.CalculateAmountToPay)
            {
                var vehicleType = requestData.VehicleType;
                var rate = parkingRatesProvider.GetRateByVehicleType(vehicleType);

                return (totalMinutes, totalMinutes * rate);
            }

            return (totalMinutes, 0);
        }

        private int GetTotalMinutesInParking(IEnumerable<VehicleStayTimeRange> timeRanges)
            => (int)Math.Ceiling(timeRanges.Sum(timeRange => timeRange.ExitTime.Subtract(timeRange.EntryTime).TotalMinutes)); // TODO: Check nullable ExitTime
    }
}
