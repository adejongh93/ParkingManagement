using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Database.DataModels
{
    public class VehicleStays
    {
        [Key]
        public string LicensePlate { get; set; }

        public IEnumerable<VehicleStayTimeRange> StaysTimeRanges { get; set; }
    }
}
