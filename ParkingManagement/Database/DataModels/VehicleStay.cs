using System;
using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Database.DataModels
{
    internal class VehicleStay
    {
        [Key]
        public string LicensePlate { get; set; }

        public VehicleStayTimeRange TimeRange { get; set; }
    }
}
