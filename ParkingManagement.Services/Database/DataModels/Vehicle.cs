using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Database.Models
{
    public record Vehicle()
    {
        [Key]
        public string LicensePlate { get; set; }

        public VehicleType Type { get; set; }
    }
}
