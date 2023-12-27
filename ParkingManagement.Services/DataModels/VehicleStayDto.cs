namespace ParkingManagement.Services.DataModels
{
    public class VehicleStayDto
    {
        public string Id { get; set; }

        public string LicensePlate { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime? ExitTime { get; set; }

        public bool StayCompleted { get => ExitTime is not null; }
    }
}
