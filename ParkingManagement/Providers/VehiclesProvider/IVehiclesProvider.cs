using ParkingManagement.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.Providers.VehicleProvider
{
    public interface IVehiclesProvider
    {
        Task<Vehicle> GetVehicleAsync(string licensePlate);

        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();

        Task<int> GetVehiclesCountAsync();
    }
}
