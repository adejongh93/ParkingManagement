using ParkingManagement.Database.Models;

namespace ParkingManagement.Providers.ParkingRatesProvider
{
    public interface IParkingRatesProvider
    {
        double GetRateByVehicleType(VehicleType vehicleType);
    }
}
