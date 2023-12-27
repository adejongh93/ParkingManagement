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

        public Task<DetailedInvoice> GenerateDetailedInvoiceAsync()
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

        public async Task<IEnumerable<StayInvoice>> GenerateInvoicesForResidentsAsync()
        {
            var residentsLicensePlates = (await GetAllLicensePlatesFromResidents()).ToList();

            var residentStays = await GetResidentsStays(residentsLicensePlates);

            residentStays = residentStays.Select(stay =>
            {
                if (!stay.StayCompleted)
                {
                    stay.ExitTime = DateTime.UtcNow;
                }
                return stay;
            });

            return GenerateInvoices(residentStays, VehicleType.RESIDENT, true);
        }

        private async Task<IEnumerable<string>> GetAllLicensePlatesFromResidents()
            => (await vehicleProvider.GetAllAsync())
                .Where(vehicle => vehicle.Type == VehicleType.RESIDENT)
                .Select(vehicle => vehicle.LicensePlate);

        private async Task<IEnumerable<VehicleStay>> GetResidentsStays(IList<string> residentsLicensePlates)
        {
            var stays = (await vehicleStaysProvider.GetAllAsync()).ToList();
            return stays.Where(stay => residentsLicensePlates.Contains(stay.LicensePlate));
        }

        private IEnumerable<StayInvoice> GenerateInvoices(IEnumerable<VehicleStay> vehicleStays, VehicleType vehicleType, bool calculateAmountToPay)
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
