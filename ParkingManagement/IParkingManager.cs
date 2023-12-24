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
        public Task<IEnumerable<Payment>> GenerateResidentsPaymentsAsync();

        public Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();

        public Task<int> GetVehiclesCountAsync();

        public Task RegisterEntryAsync(string licensePlate);

        public Task RegisterExitAsync(string licensePlate);

        public Task RegisterOfficialVehicleAsync(string licensePlate);

        public Task RegisterResidentVehicleAsync(string licensePlate);

        public Task ResetAsync();
    }
}
