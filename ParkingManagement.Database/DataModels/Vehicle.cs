using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Database.DataModels
{
    public record Vehicle()
    {
        [Key]
        public string LicensePlate { get; set; }

        public VehicleType Type { get; set; }
    }
}
