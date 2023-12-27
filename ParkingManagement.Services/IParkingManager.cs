using Microsoft.AspNetCore.Mvc;
using ParkingManagement.CommonLibrary;
using ParkingManagement.Database.DataModels;
using ParkingManagement.Services.DataModels;
using ParkingManagement.Services.Services.SystemReset.DataModels;

namespace ParkingManagement.Services
{
    public interface IParkingManager
    {
        Task<FileContentResult> GenerateResidentsPaymentsAsync(string fileName);

        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();

        IEnumerable<VehicleStay> GetAllVehiclesInParking();

        Task<IEnumerable<VehicleStay>> GetAllVehicleStaysAsync();

        Task<int> GetVehiclesCountAsync();

        Task RegisterEntryAsync(string licensePlate);

        Task<StayInvoice> RegisterExitAsync(string licensePlate);

        Task RegisterVehicleAsync(string licensePlate, VehicleType vehicleType);

        Task ExecuteResetAsync(ResetType resetType);
    }
}
