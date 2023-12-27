using ParkingManagement.Common;

namespace ParkingManagement.Services.DataModels
{
    public class DetailedStayInvoice : StayInvoice
    {
        public VehicleType VehicleType { get; set; }

        public IEnumerable<VehicleStayTimeRange> VehicleStays { get; set; }

        public double AmountToPayPerMinute { get; set; }
    }
}
