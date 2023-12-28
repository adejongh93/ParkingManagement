using ParkingManagement.CommonLibrary;
using ParkingManagement.Database.DataModels;
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

        public StayInvoice GenerateInvoice(InvoiceRequestData creationData)
        {
            var (totalMinutes, totalAmountToPay) = GetInvoiceDetails(creationData);

            return new StayInvoice()
            {
                LicensePlate = creationData.LicensePlate,
                TotalTimeInMinutes = totalMinutes,
                TotalAmountToPay = totalAmountToPay
            };
        }

        public async Task<IEnumerable<StayInvoice>> GenerateOverallInvoicesByVehicleTypeAsync(VehicleType vehicleType)
        {
            var licensePlatesByVehicleType = vehicleProvider.GetAllLicensePlatesByVehicleTypeAsync(vehicleType).ToList();

            var stays = await GetStays(licensePlatesByVehicleType);

            stays = stays.Select(stay =>
            {
                if (!stay.StayCompleted)
                {
                    stay.ExitTime = DateTime.UtcNow;
                }
                return stay;
            });

            return GenerateInvoices(stays, vehicleType, true);
        }

        private async Task<IEnumerable<VehicleStayDto>> GetStays(IEnumerable<string> licensePlates)
        {
            var stays = await vehicleStaysProvider.GetAllAsync();
            return stays.Where(stay => licensePlates.Contains(stay.LicensePlate));
        }

        private IEnumerable<StayInvoice> GenerateInvoices(IEnumerable<VehicleStayDto> vehicleStays, VehicleType vehicleType, bool calculateAmountToPay)
        {
            var groups = vehicleStays.GroupBy(stay => stay.LicensePlate);

            return groups.Select(group => GenerateInvoice(new InvoiceRequestData()
            {
                LicensePlate = group.Key,
                VehicleType = vehicleType,
                CalculateAmountToPay = calculateAmountToPay,
                StaysTimeRanges = group.Select(stay => new VehicleStayTimeRange()
                {
                    EntryTime = stay.EntryTime,
                    ExitTime = (DateTime)stay.ExitTime // TODO: Check null here
                })
            }));
        }

        private (int TotalMinutesInParking, double TotalAmountToPay) GetInvoiceDetails(InvoiceRequestData creationData)
        {
            var totalMinutes = GetTotalMinutesInParking(creationData.StaysTimeRanges);

            if (creationData.CalculateAmountToPay)
            {
                var vehicleType = creationData.VehicleType;
                var rate = parkingRatesProvider.GetRateByVehicleType(vehicleType);

                return (totalMinutes, totalMinutes * rate);
            }

            return (totalMinutes, 0);
        }

        private int GetTotalMinutesInParking(IEnumerable<VehicleStayTimeRange> timeRanges)
            => (int)Math.Ceiling(timeRanges.Sum(timeRange => timeRange.ExitTime.Subtract(timeRange.EntryTime).TotalMinutes)); // TODO: Check nullable ExitTime
    }
}
