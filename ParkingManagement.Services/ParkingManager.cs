using Microsoft.AspNetCore.Mvc;
using ParkingManagement.Database.DataModels;
using ParkingManagement.Providers.VehicleStaysProvider;
using ParkingManagement.Services.DataModels;
using ParkingManagement.Services.Providers.VehiclesInParkingProvider;
using ParkingManagement.Services.Providers.VehiclesProvider;
using ParkingManagement.Services.Services.FileManagement;
using ParkingManagement.Services.Services.Invoice;
using ParkingManagement.Services.Services.Invoice.Models;
using ParkingManagement.Services.Services.ParkingAccess;
using ParkingManagement.Services.Services.SystemReset;
using ParkingManagement.Services.Services.SystemReset.DataModels;
using ParkingManagement.Services.Services.Validations;
using ParkingManagement.Services.Services.VehicleRegistration;
using ParkingManagement.Services.Services.VehicleStays;

namespace ParkingManagement.Services
{
    public class ParkingManager : IParkingManager
    {
        private readonly IVehiclesProvider vehicleProvider;
        private readonly IVehicleStaysProvider vehicleStaysProvider;
        private readonly IVehiclesInParkingProvider vehiclesInParkingProvider;

        private readonly IValidationsService valitationsService;
        private readonly IVehicleRegistrationService vehicleRegistrationService;
        private readonly IParkingAccessService parkingAccessService;
        private readonly IVehicleStaysService vehicleStaysService;
        private readonly IInvoiceService invoiceService;
        private readonly IResetService resetService;
        private readonly IFileManagementService fileManagementService;

        public ParkingManager(IValidationsService valitationsService,
            IVehicleRegistrationService vehicleRegistrationService,
            IParkingAccessService parkingAccessService,
            IVehicleStaysService vehicleStaysService,
            IVehiclesProvider vehicleProvider,
            IVehiclesInParkingProvider vehiclesInParkingProvider,
            IVehicleStaysProvider vehicleStaysProvider,
            IInvoiceService invoiceService,
            IResetService resetService,
            IFileManagementService fileManagementService)
        {
            this.valitationsService = valitationsService;
            this.vehicleRegistrationService = vehicleRegistrationService;
            this.parkingAccessService = parkingAccessService;
            this.vehicleStaysService = vehicleStaysService;
            this.vehicleProvider = vehicleProvider;
            this.vehiclesInParkingProvider = vehiclesInParkingProvider;
            this.vehicleStaysProvider = vehicleStaysProvider;
            this.invoiceService = invoiceService;
            this.resetService = resetService;
            this.fileManagementService = fileManagementService;
        }

        public async Task<FileContentResult> GenerateResidentsPaymentsAsync(string fileName)
        {
            var invoices = await invoiceService.GenerateInvoicesForResidentsAsync();
            return fileManagementService.DownloadResidentsPayments(fileName, invoices);
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
            ValidateLicensePlate(licensePlate);
            await parkingAccessService.RegisterVehicleEntryAsync(licensePlate);
        }

        public async Task<StayInvoice> RegisterExitAsync(string licensePlate)
        {
            ValidateLicensePlate(licensePlate);

            var vehicleStay = await parkingAccessService.RegisterVehicleExitAsync(licensePlate);

            await vehicleStaysService.AddVehicleStayAsync(vehicleStay);

            return await GenerateInvoiceIfApplicableAsync(licensePlate, vehicleStay.TimeRange);
        }

        public async Task RegisterVehicleAsync(string licensePlate, VehicleType vehicleType)
        {
            ValidateLicensePlate(licensePlate);

            await vehicleRegistrationService.RegisterVehicleInTheSystemAsync(licensePlate, vehicleType);
        }

        public async Task ExecuteResetAsync(ResetType resetType)
        {
            switch (resetType)
            {
                case ResetType.PARTIAL:
                    await resetService.ExecutePartialResetAsync();
                    break;
                case ResetType.FULL:
                    await resetService.ExecuteFullResetAsync();
                    break;
            }
        }

        public async Task<IEnumerable<VehicleInParking>> GetAllVehiclesInParkingAsync()
        {
            return await vehiclesInParkingProvider.GetAllVehiclesInParkingAsync();
        }

        public async Task<IEnumerable<VehicleStay>> GetAllVehicleStaysAsync()
        {
            return await vehicleStaysProvider.GetAllVehicleStaysAsync();
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

        private void ValidateLicensePlate(string licensePlate)
        {
            if (!valitationsService.IsValidLicensePlate(licensePlate))
            {
                throw new InvalidOperationException($"License Plate {licensePlate} does not have correct format.");
            }
        }
    }
}
