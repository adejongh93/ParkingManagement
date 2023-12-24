using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.DataModels
{
    public class DetailedInvoice : Invoice
    {
        public VehicleType VehicleType { get; set; }

        public IEnumerable<VehicleStayTimeRange> VehicleStays {  get; set; }

        public double AmountToPayPerMinute { get; set; }
    }
}
