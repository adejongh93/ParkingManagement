using ParkingManagement.Database.Database.DataModels;

namespace ParkingManagement.Services.Services.VehicleStays
{
    public interface IVehicleStaysService
    {
        Task AddVehicleStayAsync(VehicleStay vehicleStay);

        Task ClearAllVehicleStaysAsync();
    }
}
