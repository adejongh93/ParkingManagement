using ParkingManagement.Database.DataModels;
using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Services.Services.VehicleStays
{
    public interface IVehicleStaysService
    {
        Task AddyAsync(VehicleStay vehicleStay);

        Task UpdateAsync(VehicleStay vehicleStay);

        Task DeleteAllCompletedStays();

        Task ResetEntryTimeForNotCompletedStaysAsync();

        bool IsVehicleInParking(string licensePlate);

        Task ClearAsync();

        VehicleStay GetVehicleNotCompletedStay(string licensePlate);
    }
}
