using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using ParkingManagement.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement
{
    public interface IParkingManager
    {
        Task<IEnumerable<Invoice>> GenerateResidentsPaymentsAsync();

        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();

        Task<IEnumerable<VehicleInParking>> GetAllVehiclesInParkingAsync();

        Task<IEnumerable<VehicleStay>> GetAllVehiclesStayAsync();

        Task<int> GetVehiclesCountAsync();

        Task RegisterEntryAsync(string licensePlate);

        Task<Invoice> RegisterExitAsync(string licensePlate);

        Task RegisterOfficialVehicleAsync(string licensePlate);

        Task RegisterResidentVehicleAsync(string licensePlate);

        Task ResetAsync();
    }
}
