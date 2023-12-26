using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Database.Database.DataModels
{
    public class VehicleStayTimeRange
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime ExitTime { get; set; }
    }
}
