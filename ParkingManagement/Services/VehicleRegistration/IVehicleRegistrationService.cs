using ParkingManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Services.VehicleRegistration
{
    public interface IVehicleRegistrationService
    {
        Task RegisterVehicleInTheSystemAsync(string licensePlate, VehicleType vehicleType);

        Task<bool> IsVehicleRegisteredInTheSystemAsync(string licensePlate);
    }
}
