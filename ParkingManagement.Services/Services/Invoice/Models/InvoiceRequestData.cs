using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Services.Models
{
    public class InvoiceRequestData
    {
        public string LicensePlate {  get; set; }

        public VehicleType VehicleType { get; set; }

        public IEnumerable<VehicleStayTimeRange> StaysTimeRanges { get; set; }
    }
}
