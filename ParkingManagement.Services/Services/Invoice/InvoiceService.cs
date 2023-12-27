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

        public StayInvoice GenerateInvoiceIfApplicable(InvoiceRequestData creationData)
        {
            var vehicleType = creationData.VehicleType;
            var rate = parkingRatesProvider.GetRateByVehicleType(vehicleType);
            var totalMinutes = (int)Math.Ceiling(creationData.StaysTimeRanges.Sum(timeRange => timeRange.ExitTime.Subtract(timeRange.EntryTime).TotalMinutes)); // TODO: Check nullable ExitTime

            return new StayInvoice()
            {
                LicensePlase = creationData.LicensePlate,
                TotalTimeInMinutes = totalMinutes,
                TotalAmountToPay = totalMinutes * rate
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

            return GenerateInvoices(residentStays, VehicleType.RESIDENT);
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

        private IEnumerable<StayInvoice> GenerateInvoices(IEnumerable<VehicleStay> vehicleStays, VehicleType vehicleType)
        {
            var groups = vehicleStays.GroupBy(vehicle => vehicle.LicensePlate);

            return groups.Select(group => GenerateInvoiceIfApplicable(new InvoiceRequestData()
            {
                LicensePlate = group.Key,
                VehicleType = vehicleType,
                StaysTimeRanges = group.Select(stay => new VehicleStayTimeRange()
                {
                    EntryTime = stay.EntryTime,
                    ExitTime = (DateTime)stay.ExitTime // TODO: Check null here
                })
            }));
        }
    }
}
