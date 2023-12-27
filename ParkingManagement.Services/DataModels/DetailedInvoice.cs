using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Services.DataModels
{
    public class DetailedInvoice : StayInvoice
    {
        public VehicleType VehicleType { get; set; }

        //public IEnumerable<VehicleStayTimeRange> VehicleStays { get; set; }

        public double AmountToPayPerMinute { get; set; }
    }
}
