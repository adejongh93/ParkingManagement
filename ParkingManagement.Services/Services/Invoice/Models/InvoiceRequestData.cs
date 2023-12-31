﻿using ParkingManagement.CommonLibrary;
using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Services.Services.Invoice.Models
{
    public class InvoiceRequestData
    {
        public string LicensePlate { get; set; }

        public VehicleType VehicleType { get; set; }

        public IEnumerable<VehicleStayTimeRange> StaysTimeRanges { get; set; }

        public bool CalculateAmountToPay {  get; set; }
    }
}
