using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.DataModels
{
    public class Payment
    {
        public string LicensePlate { get; set; }

        public int TimeInParking { get; set; }

        public double TotalToPay { get; set; }
    }
}
