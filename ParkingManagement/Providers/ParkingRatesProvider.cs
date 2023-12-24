using ParkingManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Providers
{
    public class ParkingRatesProvider : IParkingRatesProvider
    {
        // If these values are intended to change, maybe it's better to put it in the DB
        public double GetRateByVehicleType(VehicleType vehicleType)
            => vehicleType switch
            {
                VehicleType.Default => 0.5,
                VehicleType.Resident => 0.05,
                VehicleType.Official => 0,
                _ => throw new NotImplementedException()
            };
    }
}
