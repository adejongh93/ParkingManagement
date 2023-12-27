using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Repositories;

namespace ParkingManagement.Services.Services.VehicleRegistration
{
    internal class VehicleRegistrationService : IVehicleRegistrationService
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehicleRegistrationService(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public async Task RegisterVehicleInTheSystemAsync(string licensePlate, VehicleType vehicleType)
        {
            await CheckVehicleRegistrationAsync(licensePlate);
            await RegisterVehicleAsync(licensePlate, vehicleType);
        }

        public async Task<bool> IsVehicleRegisteredInTheSystemAsync(string licensePlate)
        {
            return await vehicleRepository.ExistsByLicensePlateAsync(licensePlate);
        }

        private async Task CheckVehicleRegistrationAsync(string licensePlate)
        {
            if (await IsVehicleRegisteredInTheSystemAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is already registered in the system.");
            }
        }

        private async Task RegisterVehicleAsync(string licensePlate, VehicleType vehicleType)
        {
            await vehicleRepository.AddAsync(new Vehicle()
            {
                LicensePlate = licensePlate,
                Type = vehicleType
            });
        }
    }
}
