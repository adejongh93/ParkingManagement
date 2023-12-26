using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.Providers.VehicleStaysProvider
{
    public interface IVehicleStaysProvider
    {
        Task<VehicleStay> GetVehicleStayAsync(string licensePlate);

        Task<IEnumerable<VehicleStay>> GetAllVehicleStaysAsync();

        Task<int> GetVehicleStaysCountAsync();
    }
}
