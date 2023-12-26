using ParkingManagement.Database.Database.DataModels;

namespace ParkingManagement.Services.Providers.ParkingRatesProvider
{
    public class ParkingRatesProvider : IParkingRatesProvider
    {
        // If these values are intended to change, maybe it's better to put them in the DB
        public double GetRateByVehicleType(VehicleType vehicleType)
            => vehicleType switch
            {
                VehicleType.EXTERNAL => 0.5,
                VehicleType.RESIDENT => 0.05,
                VehicleType.OFFICIAL => 0,
                _ => throw new NotImplementedException()
            };
    }
}
