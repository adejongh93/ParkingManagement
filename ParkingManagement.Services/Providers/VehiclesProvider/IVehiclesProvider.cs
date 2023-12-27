using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Services.Providers.VehiclesProvider
{
    public interface IVehiclesProvider
    {
        Task<Vehicle> FindAsync(string licensePlate);

        Task<IEnumerable<Vehicle>> GetAllAsync();

        Task<int> CountAsync();

        Task<VehicleType> GetVehicleTypeAsync(string licensePlate);
    }
}
