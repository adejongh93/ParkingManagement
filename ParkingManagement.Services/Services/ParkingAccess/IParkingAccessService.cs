using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Services.Services.ParkingAccess
{
    public interface IParkingAccessService
    {
        Task RegisterVehicleEntryAsync(string licensePlate);

        Task<VehicleStayDto> RegisterVehicleExitAsync(string licensePlate);
    }
}
