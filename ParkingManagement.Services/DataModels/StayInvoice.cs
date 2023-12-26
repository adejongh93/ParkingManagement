using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.DataModels
{
    public class StayInvoice
    {
        public string LicensePlase {  get; set; }

        public int TotalTimeInMinutes { get; set; }

        public double TotalAmountToPay {  get; set; }
    }
}
