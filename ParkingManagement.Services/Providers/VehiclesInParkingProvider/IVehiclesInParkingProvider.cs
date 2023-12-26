using ParkingManagement.Database.Database.DataModels;

namespace ParkingManagement.Services.Providers.VehiclesInParkingProvider
{
    public interface IVehiclesInParkingProvider
    {
        Task<VehicleInParking> GetVehicleInParkingAsync(string licensePlate);

        Task<IEnumerable<VehicleInParking>> GetAllVehiclesInParkingAsync();

        Task<int> GetVehiclesInParkingCountAsync();
    }
}
