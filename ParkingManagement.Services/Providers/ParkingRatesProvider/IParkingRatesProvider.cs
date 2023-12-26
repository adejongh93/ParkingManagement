using ParkingManagement.Database.Database.DataModels;

namespace ParkingManagement.Services.Providers.ParkingRatesProvider
{
    public interface IParkingRatesProvider
    {
        double GetRateByVehicleType(VehicleType vehicleType);
    }
}
