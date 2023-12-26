using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using ParkingManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Providers.VehiclesInParkingProvider
{
    public class VehiclesInParkingProvider : IVehiclesInParkingProvider
    {
        private readonly IVehiclesInParkingRepository vehiclesInParkingRepository;

        public VehiclesInParkingProvider(IVehiclesInParkingRepository vehiclesInParkingRepository)
        {
            this.vehiclesInParkingRepository = vehiclesInParkingRepository;
        }

        public async Task<IEnumerable<VehicleInParking>> GetAllVehiclesInParkingAsync()
        {
            return await vehiclesInParkingRepository.GetAllAsync();
        }

        public async Task<VehicleInParking> GetVehicleInParkingAsync(string licensePlate)
        {
            return await vehiclesInParkingRepository.GetAsync(licensePlate);
        }

        public Task<int> GetVehiclesInParkingCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
