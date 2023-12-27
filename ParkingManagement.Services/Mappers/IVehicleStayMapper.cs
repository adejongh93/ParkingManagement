using ParkingManagement.Database.DataModels;
using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Services.Mappers
{
    internal interface IVehicleStayMapper
    {
        VehicleStayDto Map(VehicleStay vehicle);

        VehicleStay Map(VehicleStayDto vehicle);
    }
}
