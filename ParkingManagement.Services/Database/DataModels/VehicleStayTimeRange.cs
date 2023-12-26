using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Database.DataModels
{
    public class VehicleStayTimeRange
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime EntryTime { get; set; }

        public DateTime ExitTime { get; set; }
    }
}
