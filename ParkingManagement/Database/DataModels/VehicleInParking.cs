using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Database.DataModels
{
    internal class VehicleInParking
    {
        [Key]
        public string LicensePlate { get; set; }

        public DateTime EntryTime { get; set; }
    }
}
