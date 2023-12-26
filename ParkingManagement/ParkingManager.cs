using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using ParkingManagement.DataModels;
using ParkingManagement.Providers.VehicleProvider;
using ParkingManagement.Providers.VehiclesInParkingProvider;
using ParkingManagement.Providers.VehicleStaysProvider;
using ParkingManagement.Services.Invoice;
using ParkingManagement.Services.Models;
using ParkingManagement.Services.ParkingAccess;
using ParkingManagement.Services.SystemReset;
using ParkingManagement.Services.VehicleRegistration;
using ParkingManagement.Services.VehicleStays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManagement
{
    internal class ParkingManager : IParkingManager
    {
        private readonly IVehiclesProvider vehicleProvider;
        private readonly IVehicleStaysProvider vehicleStaysProvider;
        private readonly IVehiclesInParkingProvider vehiclesInParkingProvider;

        private readonly IVehicleRegistrationService vehicleRegistrationService;
        private readonly IParkingAccessService parkingAccessService;
        private readonly IVehicleStaysService vehicleStaysService;
        private readonly IInvoiceService invoiceService;
        private readonly IResetService resetService;

        public ParkingManager(IVehicleRegistrationService vehicleRegistrationService,
            IParkingAccessService parkingAccessService,
            IVehicleStaysService vehicleStaysService,
            IVehiclesProvider vehicleProvider,
            IVehiclesInParkingProvider vehiclesInParkingProvider,
            IVehicleStaysProvider vehicleStaysProvider,
            IInvoiceService invoiceService,
            IResetService resetService)
        {
            this.vehicleRegistrationService = vehicleRegistrationService;
            this.parkingAccessService = parkingAccessService;
            this.vehicleStaysService = vehicleStaysService;
            this.vehicleProvider = vehicleProvider;
            this.vehiclesInParkingProvider = vehiclesInParkingProvider;
            this.vehicleStaysProvider = vehicleStaysProvider;
            this.invoiceService = invoiceService;
            this.resetService = resetService;
        }

        public async Task<IEnumerable<StayInvoice>> GenerateResidentsPaymentsAsync()
        {
            return await invoiceService.GenerateInvoicesForResidentsAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await vehicleProvider.GetAllVehiclesAsync();
        }

        public async Task<int> GetVehiclesCountAsync()
        {
            return await vehicleProvider.GetVehiclesCountAsync();
        }

        public async Task RegisterEntryAsync(string licensePlate)
        {
            await parkingAccessService.RegisterVehicleEntryAsync(licensePlate);
        }

        public async Task<StayInvoice> RegisterExitAsync(string licensePlate)
        {
            var vehicleStay = await parkingAccessService.RegisterVehicleExitAsync(licensePlate);

            await vehicleStaysService.AddVehicleStayAsync(vehicleStay);

            return await GenerateInvoiceIfApplicableAsync(licensePlate, vehicleStay.TimeRange);
        }

        public async Task RegisterOfficialVehicleAsync(string licensePlate)
            => await vehicleRegistrationService.RegisterVehicleInTheSystemAsync(licensePlate, VehicleType.Official);

        public async Task RegisterResidentVehicleAsync(string licensePlate)
            => await vehicleRegistrationService.RegisterVehicleInTheSystemAsync(licensePlate, VehicleType.Resident);

        public async Task ExecutePartialResetAsync()
        {
            await resetService.ExecutePartialResetAsync();
        }

        private async Task<StayInvoice> GenerateInvoiceIfApplicableAsync(string licensePlate, VehicleStayTimeRange timeRange)
        {
            var vehicleType = (await vehicleProvider.GetVehicleAsync(licensePlate)).Type;

            return invoiceService.GenerateInvoiceIfApplicable(new InvoiceRequestData()
            {
                LicensePlate = licensePlate,
                VehicleType = vehicleType,
                StaysTimeRanges = new List<VehicleStayTimeRange>() { timeRange }
            });
        }

        public async Task<IEnumerable<VehicleInParking>> GetAllVehiclesInParkingAsync()
        {
            return await vehiclesInParkingProvider.GetAllVehiclesInParkingAsync();
        }

        public async Task<IEnumerable<VehicleStay>> GetAllVehicleStaysAsync()
        {
            return await vehicleStaysProvider.GetAllVehicleStaysAsync();
        }
    }
}
