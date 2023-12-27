using ParkingManagement.Database.DataModels;
using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Services.Mappers
{
    internal interface IVehicleMapper
    {
        VehicleDto Map(Vehicle vehicle);
    }
}
