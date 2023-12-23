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

        public ParkingManager(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public Task GenerateResidentsPaymentsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VehicleDataModel>> GetAllVehiclesAsync()
        {
            return await vehicleRepository.GetAllAsync();
        }

        public async Task<int> GetVehiclesCountAsync()
        {
            return await vehicleRepository.GetCountAsync();
        }

        public Task RegisterEntryAsync()
        {
            throw new NotImplementedException();
        }

        public Task RegisterExitAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> TryRegisterOfficialVehicleAsync(string licensePlate)
            =>  await TryRegisterVehicleAsync(licensePlate, VehicleTypeDataModel.Official);

        public async Task<bool> TryRegisterResidentVehicleAsync(string licensePlate)
            => await TryRegisterVehicleAsync(licensePlate, VehicleTypeDataModel.Resident);

        public Task ResetAsync()
        {
            throw new NotImplementedException();
        }

        private async Task<bool> TryRegisterVehicleAsync(string licensePlate, VehicleTypeDataModel vehicleType)
        {
            return await vehicleRepository.TryAddAsync(new VehicleDataModel()
            {
                LicensePlate = licensePlate,
                Type = vehicleType
            });
        }
    }
}
