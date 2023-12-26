using ParkingManagement.Database.DataModels;
using ParkingManagement.Repositories;
using ParkingManagement.Services.VehicleRegistration;
using System;
using System.Threading.Tasks;

namespace ParkingManagement.Services.ParkingAccess
{
    public class ParkingAccessService : IParkingAccessService
    {
        private readonly IVehicleRegistrationService vehicleRegistrationService;

        private readonly IVehiclesInParkingRepository vehiclesInParkingRepository;

        public ParkingAccessService(IVehicleRegistrationService vehicleRegistrationService,
            IVehiclesInParkingRepository vehiclesInParkingRepository)
        {
            this.vehicleRegistrationService = vehicleRegistrationService;
            this.vehiclesInParkingRepository = vehiclesInParkingRepository;
        }

        public async Task RegisterVehicleEntryAsync(string licensePlate)
        {
            if (!await IsVehicleRegisteredInTheSystem(licensePlate))
            {
                // Vehicle is not registered in the system. It will be registered as External
                await vehicleRegistrationService.RegisterVehicleInTheSystemAsync(licensePlate, Database.Models.VehicleType.EXTERNAL);
            }

            if (await VehicleInParkingAsync(licensePlate))
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

            if (!await VehicleInParkingAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is not in the parking.");
            }

            return await RegisterExitAsync(licensePlate);
        }

        private async Task<bool> IsVehicleRegisteredInTheSystem(string licensePlate)
        {
            return await vehicleRegistrationService.IsVehicleRegisteredInTheSystemAsync(licensePlate);
        }

        private async Task<bool> VehicleInParkingAsync(string licensePlate)
        {
            return await vehiclesInParkingRepository.ExistsAsync(licensePlate);
        }

        private async Task RegisterEntryAsync(string licensePlate)
        {
            await vehiclesInParkingRepository.AddAsync(new VehicleInParking()
            {
                LicensePlate = licensePlate,
                EntryTime = DateTime.UtcNow
            });
        }

        private async Task<VehicleStay> RegisterExitAsync(string licensePlate)
        {
            var vehicle = await vehiclesInParkingRepository.GetAsync(licensePlate);
            await RemoveVehicleFromParkingAsync(vehicle);

            return new VehicleStay()
            {
                LicensePlate = licensePlate,
                TimeRange = new VehicleStayTimeRange()
                {
                    EntryTime = vehicle.EntryTime,
                    ExitTime = DateTime.UtcNow
                }
            };
        }

        private async Task RemoveVehicleFromParkingAsync(VehicleInParking vehicleInParking)
        {
            await vehiclesInParkingRepository.RemoveAsync(vehicleInParking);
        }
    }
}
