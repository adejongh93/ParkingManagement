using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Database.Database.DataModels
{
    public class VehicleStay
    {
        [Key]
        public string LicensePlate { get; set; }

        public VehicleStayTimeRange TimeRange { get; set; }
    }
}
