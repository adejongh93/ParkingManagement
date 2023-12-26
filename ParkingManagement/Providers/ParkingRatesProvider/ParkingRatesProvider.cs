using ParkingManagement.Database.Models;
using System;

namespace ParkingManagement.Providers.ParkingRatesProvider
{
    public class ParkingRatesProvider : IParkingRatesProvider
    {
        // If these values are intended to change, maybe it's better to put them in the DB
        public double GetRateByVehicleType(VehicleType vehicleType)
            => vehicleType switch
            {
                VehicleType.External => 0.5,
                VehicleType.Resident => 0.05,
                VehicleType.Official => 0,
                _ => throw new NotImplementedException()
            };
    }
}
