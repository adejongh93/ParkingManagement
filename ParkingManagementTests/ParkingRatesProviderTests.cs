using ParkingManagement.Database.Models;
using ParkingManagement.Providers.ParkingRatesProvider;

namespace ParkingManagementTests
{
    public class ParkingRatesProviderTests
    {
        [Theory]
        [InlineData(VehicleType.EXTERNAL, 0.5)]
        [InlineData(VehicleType.RESIDENT, 0.05)]
        [InlineData(VehicleType.OFFICIAL, 0)]
        public void GetRateByVehicleType_ShouldReturnCorrectRates(VehicleType vehicleType, double expectedRate)
        {
            // Arrange
            var parkingRatesProvider = new ParkingRatesProvider();

            // Act
            var rate = parkingRatesProvider.GetRateByVehicleType(vehicleType);

            // Assert
            rate.Equals(expectedRate);
        }
    }
}