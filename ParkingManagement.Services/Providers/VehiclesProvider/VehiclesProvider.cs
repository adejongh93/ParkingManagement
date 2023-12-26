using ParkingManagement.Database.Database.DataModels;
using ParkingManagement.Database.Repositories;

namespace ParkingManagement.Services.Providers.VehiclesProvider
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
