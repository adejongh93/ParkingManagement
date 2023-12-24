using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using ParkingManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement
{
    internal class ParkingManager : IParkingManager
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IVehicleInParkingRepository vehiclesInParkingRepository;
        private readonly IVehicleStayRepository vehicleStayRepository;

        public ParkingManager(IVehicleRepository vehicleRepository,
            IVehicleInParkingRepository vehicleInParkingRepository,
            IVehicleStayRepository vehicleStayRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehiclesInParkingRepository = vehicleInParkingRepository;
            this.vehicleStayRepository = vehicleStayRepository;
        }

        public Task GenerateResidentsPaymentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await vehicleRepository.GetAllAsync();
        }

        public async Task<int> GetVehiclesCountAsync()
        {
            return await vehicleRepository.GetCountAsync();
        }

        public async Task RegisterEntryAsync(string licensePlate)
        {
            if (!await vehicleRepository.ExistsAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is not registered in the system.");
            }

            if (await vehiclesInParkingRepository.ExistsAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is already in the parking.");
            }

            await vehiclesInParkingRepository.AddAsync(new VehicleInParking()
            {
                LicensePlate = licensePlate,
                EntryTime = DateTime.UtcNow
            });
        }

        public async Task RegisterExitAsync(string licensePlate)
        {
            if (!await vehicleRepository.ExistsAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is not registered in the system.");
            }

            if (!await vehiclesInParkingRepository.ExistsAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is not in the parking.");
            }

            var vehicle = await vehiclesInParkingRepository.GetAsync(licensePlate);

            await vehicleStayRepository.AddAsync(new VehicleStay()
            {
                LicensePlate = licensePlate,
                EntryTime = vehicle.EntryTime,
                ExitTime = DateTime.UtcNow
            });

            await vehiclesInParkingRepository.RemoveAsync(vehicle);
        }

        public async Task RegisterOfficialVehicleAsync(string licensePlate)
            =>  await RegisterVehicleAsync(licensePlate, VehicleType.Official);

        public async Task RegisterResidentVehicleAsync(string licensePlate)
            => await RegisterVehicleAsync(licensePlate, VehicleType.Resident);

        public Task ResetAsync()
        {
            throw new NotImplementedException();
        }

        private async Task RegisterVehicleAsync(string licensePlate, VehicleType vehicleType)
        {
            if (await vehicleRepository.ExistsAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is already registered in the system.");
            }

            await vehicleRepository.AddAsync(new Vehicle()
            {
                LicensePlate = licensePlate,
                Type = vehicleType
            });
        }
    }
}
