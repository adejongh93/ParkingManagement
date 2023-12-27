using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Services.Services.VehicleStays
{
    public interface IVehicleStaysService
    {
        Task AddyAsync(VehicleStayDto vehicleStay);

        Task UpdateAsync(VehicleStayDto vehicleStay);

        Task DeleteAllCompletedStays();

        Task ResetEntryTimeForNotCompletedStaysAsync();

        bool IsVehicleInParking(string licensePlate);

        Task ClearAsync();

        VehicleStayDto GetVehicleNotCompletedStay(string licensePlate);
    }
}
