using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Database.DataModels
{
    public record VehicleInParking
    {
        [Key]
        public string LicensePlate { get; set; }

        public DateTime EntryTime { get; set; }
    }
}
