using ParkingManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement
{
    public interface IParkingManager
    {
        public Task GenerateResidentsPaymentsAsync();

        public Task<IEnumerable<VehicleDataModel>> GetAllVehiclesAsync();

        public Task<int> GetVehiclesCountAsync();

        public Task RegisterEntryAsync();

        public Task RegisterExitAsync();

        public Task<bool> TryRegisterOfficialVehicleAsync(string licensePlate);

        public Task<bool> TryRegisterResidentVehicleAsync(string licensePlate);

        public Task ResetAsync();
    }
}
