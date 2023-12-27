using ParkingManagement.CommonLibrary;

namespace ParkingManagement.Services.DataModels
{
    public record VehicleDto()
    {
        public string LicensePlate { get; set; }

        public VehicleType Type { get; set; }
    }
}
