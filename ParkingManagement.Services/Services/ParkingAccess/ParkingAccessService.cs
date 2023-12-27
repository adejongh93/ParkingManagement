using ParkingManagement.Database.DataModels;
using ParkingManagement.Providers.VehicleStaysProvider;
using ParkingManagement.Services.Services.VehicleRegistration;
using ParkingManagement.Services.Services.VehicleStays;

namespace ParkingManagement.Services.Services.ParkingAccess
{
    public class ParkingAccessService : IParkingAccessService
    {
        private readonly IVehicleRegistrationService vehicleRegistrationService;
        private readonly IVehicleStaysService vehicleStaysService;

        private readonly IVehicleStaysProvider vehicleStaysProvider;

        public ParkingAccessService(IVehicleRegistrationService vehicleRegistrationService,
            IVehicleStaysService vehicleStaysService,
            IVehicleStaysProvider vehicleStaysProvider)
        {
            this.vehicleRegistrationService = vehicleRegistrationService;
            this.vehicleStaysService = vehicleStaysService;
            this.vehicleStaysProvider = vehicleStaysProvider;
        }

        public async Task RegisterVehicleEntryAsync(string licensePlate)
        {
            if (!await IsVehicleRegisteredInTheSystem(licensePlate))
            {
                // Vehicle is not registered in the system. It will be registered as External
                await vehicleRegistrationService.RegisterVehicleInTheSystemAsync(licensePlate, VehicleType.EXTERNAL);
            }

            if (IsVehicleInParking(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is already in the parking.");
            }

            await RegisterEntryAsync(licensePlate);
        }

        public async Task<VehicleStay> RegisterVehicleExitAsync(string licensePlate)
        {
            if (!await IsVehicleRegisteredInTheSystem(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is not registered in the system.");
            }

            if (!IsVehicleInParking(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is not in the parking.");
            }

            return await RegisterExitAsync(licensePlate);
        }

        private async Task<bool> IsVehicleRegisteredInTheSystem(string licensePlate)
        {
            return await vehicleRegistrationService.IsVehicleRegisteredInTheSystemAsync(licensePlate);
        }

        private bool IsVehicleInParking(string licensePlate)
        {
            return vehicleStaysService.IsVehicleInParking(licensePlate);
        }

        private async Task RegisterEntryAsync(string licensePlate)
        {
            await vehicleStaysService.AddVehicleStayAsync(new VehicleStay()
            {
                Id = Guid.NewGuid().ToString(),
                LicensePlate = licensePlate,
                EntryTime = DateTime.UtcNow,
                ExitTime = null
            });
        }

        private async Task<VehicleStay> RegisterExitAsync(string licensePlate)
        {
            var stayToComplete = vehicleStaysService.GetVehicleNotCompletedStay(licensePlate);
            stayToComplete.ExitTime = DateTime.UtcNow;

            await vehicleStaysService.UpdateVehicleStayAsync(stayToComplete);

            return stayToComplete;
        }
    }
}
