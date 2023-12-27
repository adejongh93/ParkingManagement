namespace ParkingManagement.Services.DataModels
{
    public class StayInvoice
    {
        public string LicensePlate { get; set; }

        public int TotalTimeInMinutes { get; set; }

        public double TotalAmountToPay { get; set; }
    }
}
