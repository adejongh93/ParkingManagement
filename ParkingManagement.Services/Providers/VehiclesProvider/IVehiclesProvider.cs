using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Services.Providers.VehiclesProvider
{
    public interface IVehiclesProvider
    {
        Task<Vehicle> GetVehicleAsync(string licensePlate);

        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();

        Task<int> GetVehiclesCountAsync();
    }
}
