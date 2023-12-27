using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Repositories;

namespace ParkingManagement.Services.Providers.VehiclesProvider
{
    internal class VehiclesProvider : IVehiclesProvider
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehiclesProvider(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
            => await vehicleRepository.GetAllAsync();

        public async Task<Vehicle> FindAsync(string licensePlate)
            => await vehicleRepository.FindAsync(licensePlate);

        public async Task<int> CountAsync()
            => await vehicleRepository.CountAsync();
    }
}
