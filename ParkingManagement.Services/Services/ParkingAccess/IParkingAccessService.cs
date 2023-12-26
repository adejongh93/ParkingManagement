using ParkingManagement.Database.DataModels;
using System.Threading.Tasks;

namespace ParkingManagement.Services.ParkingAccess
{
    public interface IParkingAccessService
    {
        Task RegisterVehicleEntryAsync(string licensePlate);

        Task<VehicleStay> RegisterVehicleExitAsync(string licensePlate);
    }
}
