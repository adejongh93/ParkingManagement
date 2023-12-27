using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Services.Services.VehicleStays
{
    public interface IVehicleStaysService
    {
        Task AddVehicleStayAsync(VehicleStay vehicleStay);

        Task UpdateVehicleStayAsync(VehicleStay vehicleStay);

        Task DeleteAllCompletedStays();

        Task ResetEntryTimeForNotCompletedStaysAsync();

        bool IsVehicleInParking(string licensePlate);

        Task ClearAllVehicleStaysAsync();

        VehicleStay GetVehicleNotCompletedStay(string licensePlate);
    }
}
