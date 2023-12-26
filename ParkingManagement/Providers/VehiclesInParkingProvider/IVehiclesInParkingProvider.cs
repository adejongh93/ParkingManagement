using ParkingManagement.Database.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.Providers.VehiclesInParkingProvider
{
    public interface IVehiclesInParkingProvider
    {
        Task<VehicleInParking> GetVehicleInParkingAsync(string licensePlate);

        Task<IEnumerable<VehicleInParking>> GetAllVehiclesInParkingAsync();

        Task<int> GetVehiclesInParkingCountAsync();
    }
}
