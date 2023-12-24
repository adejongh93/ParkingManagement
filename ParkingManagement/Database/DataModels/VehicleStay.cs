using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Database.DataModels
{
    internal class VehicleStay
    {
        [Key]
        public string LicensePlate { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime ExitTime { get; set; }
    }
}
