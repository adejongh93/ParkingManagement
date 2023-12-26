using ParkingManagement.Database.Models;
using ParkingManagement.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.Providers.VehicleProvider
{
    public class VehiclesProvider : IVehiclesProvider
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehiclesProvider(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await vehicleRepository.GetAllAsync();
        }

        public async Task<Vehicle> GetVehicleAsync(string licensePlate)
        {
            return await vehicleRepository.GetAsync(licensePlate);
        }

        public async Task<int> GetVehiclesCountAsync()
        {
            return await vehicleRepository.GetCountAsync();
        }
    }
}
