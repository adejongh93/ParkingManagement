using ParkingManagement.Database.DataModels;
using System.Threading.Tasks;

namespace ParkingManagement.Services.VehicleStays
{
    public interface IVehicleStaysService
    {
        Task AddVehicleStayAsync(VehicleStay vehicleStay);

        Task ClearAllVehicleStaysAsync();
    }
}
