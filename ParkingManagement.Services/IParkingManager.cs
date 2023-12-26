using Microsoft.AspNetCore.Mvc;
using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using ParkingManagement.DataModels;
using ParkingManagement.Services.SystemReset.DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement
{
    public interface IParkingManager
    {
        Task<FileContentResult> GenerateResidentsPaymentsAsync(string fileName);

        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();

        Task<IEnumerable<VehicleInParking>> GetAllVehiclesInParkingAsync();

        Task<IEnumerable<VehicleStay>> GetAllVehicleStaysAsync();

        Task<int> GetVehiclesCountAsync();

        Task RegisterEntryAsync(string licensePlate);

        Task<StayInvoice> RegisterExitAsync(string licensePlate);

        Task RegisterVehicleAsync(string licensePlate, VehicleType vehicleType);

        Task ExecuteResetAsync(ResetType resetType);
    }
}
