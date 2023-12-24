using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Database.DataModels
{
    public class VehicleStays
    {
        [Key]
        public string LicensePlate { get; set; }

        public IEnumerable<VehicleStayTimeRange> StaysTimeRanges { get; set; }
    }
}
