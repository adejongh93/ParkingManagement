using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Database.DataModels
{
    public class VehicleStay
    {
        [Key]
        public string Id { get; set; }

        public string LicensePlate { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }
    }
}
