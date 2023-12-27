using ParkingManagement.Database.DataModels;
using ParkingManagement.Providers.VehicleStaysProvider;
using ParkingManagement.Services.DataModels;
using ParkingManagement.Services.Providers.ParkingRatesProvider;
using ParkingManagement.Services.Providers.VehiclesInParkingProvider;
using ParkingManagement.Services.Providers.VehiclesProvider;
using ParkingManagement.Services.Services.Invoice.Models;

namespace ParkingManagement.Services.Services.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IParkingRatesProvider parkingRatesProvider;
        private readonly IVehiclesProvider vehicleProvider;
        private readonly IVehicleStaysProvider vehicleStaysProvider;
        private readonly IVehiclesInParkingProvider vehiclesInParkingProvider;

        public InvoiceService(IParkingRatesProvider parkingRatesProvider,
            IVehiclesProvider vehicleProvider,
            IVehiclesInParkingProvider vehiclesInParkingProvider,
            IVehicleStaysProvider vehicleStaysProvider)
        {
            this.parkingRatesProvider = parkingRatesProvider;
            this.vehicleProvider = vehicleProvider;
            this.vehiclesInParkingProvider = vehiclesInParkingProvider;
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
            var totalMinutes = (int)creationData.StaysTimeRanges.Sum(timeRange => timeRange.ExitTime.Subtract(timeRange.EntryTime).TotalMinutes);

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

            var residentsInParking = await GetResidentsInParking(residentsLicensePlates);

            residentStays = residentStays.Union(residentsInParking.Select(residentInParking => new VehicleStay()
            {
                LicensePlate = residentInParking.LicensePlate,
                TimeRange = new VehicleStayTimeRange()
                {
                    EntryTime = residentInParking.EntryTime,
                    ExitTime = DateTime.UtcNow
                }
            }));

            return GenerateInvoices(residentStays, VehicleType.RESIDENT);
        }

        private async Task<IEnumerable<string>> GetAllLicensePlatesFromResidents()
        {
            return (await vehicleProvider.GetAllVehiclesAsync())
                .Where(vehicle => vehicle.Type == VehicleType.RESIDENT)
                .Select(vehicle => vehicle.LicensePlate);
        }

        private async Task<IEnumerable<VehicleStay>> GetResidentsStays(IList<string> residentsLicensePlates)
        {
            var stays = (await vehicleStaysProvider.GetAllVehicleStaysAsync()).ToList();
            return stays.Where(stay => residentsLicensePlates.Contains(stay.LicensePlate));
        }

        private async Task<IEnumerable<VehicleInParking>> GetResidentsInParking(IList<string> residentsLicensePlates)
        {
            var vehiclesInParking = (await vehiclesInParkingProvider.GetAllVehiclesInParkingAsync()).ToList();
            return vehiclesInParking.Where(vehicle => residentsLicensePlates.Contains(vehicle.LicensePlate)).ToList();
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
                    EntryTime = stay.TimeRange.EntryTime,
                    ExitTime = stay.TimeRange.ExitTime
                })
            }));
        }
    }
}
