using ParkingManagement.Database.DataModels;
using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Services.Services.ParkingAccess
{
    public interface IParkingAccessService
    {
        Task RegisterVehicleEntryAsync(string licensePlate);

        Task<VehicleStay> RegisterVehicleExitAsync(string licensePlate);
    }
}
