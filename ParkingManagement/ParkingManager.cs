using Microsoft.AspNetCore.Mvc;
using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using ParkingManagement.DataModels;
using ParkingManagement.Providers.VehicleProvider;
using ParkingManagement.Providers.VehiclesInParkingProvider;
using ParkingManagement.Providers.VehicleStaysProvider;
using ParkingManagement.Services.FileManagement;
using ParkingManagement.Services.Invoice;
using ParkingManagement.Services.Models;
using ParkingManagement.Services.ParkingAccess;
using ParkingManagement.Services.SystemReset;
using ParkingManagement.Services.VehicleRegistration;
using ParkingManagement.Services.VehicleStays;
using System;
using System.Collections.Generic;
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
        private readonly IFileManagementService fileManagementService;

        public ParkingManager(IVehicleRegistrationService vehicleRegistrationService,
            IParkingAccessService parkingAccessService,
            IVehicleStaysService vehicleStaysService,
            IVehiclesProvider vehicleProvider,
            IVehiclesInParkingProvider vehiclesInParkingProvider,
            IVehicleStaysProvider vehicleStaysProvider,
            IInvoiceService invoiceService,
            IResetService resetService,
            IFileManagementService fileManagementService)
        {
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

        public async Task RegisterVehicleAsync(string licensePlate, string vehicleTypeStr)
        {
            var successParsing = Enum.TryParse(vehicleTypeStr, out VehicleType vehicleType);

            if (!successParsing)
            {
                throw new InvalidOperationException($"Vehicle Type provided {vehicleTypeStr} does not exist.");
            }

            await vehicleRegistrationService.RegisterVehicleInTheSystemAsync(licensePlate, vehicleType);
        }

        public async Task ExecutePartialResetAsync()
        {
            await resetService.ExecutePartialResetAsync();
        }

        public async Task ExecuteFullResetAsync()
        {
            await resetService.ExecuteFullResetAsync();
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
