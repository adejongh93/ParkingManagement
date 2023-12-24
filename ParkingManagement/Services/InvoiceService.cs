using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using ParkingManagement.DataModels;
using ParkingManagement.Providers;
using ParkingManagement.Repositories;
using ParkingManagement.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IParkingRatesProvider _parkingRatesProvider;

        public InvoiceService(IParkingRatesProvider parkingRatesProvider)
        {
            _parkingRatesProvider = parkingRatesProvider;
        }


        public Task<DetailedInvoice> GenerateDetailedInvoiceAsync()
        {
            throw new NotImplementedException();
        }

        public Invoice GenerateInvoice(InvoiceCreationData creationData)
        {
            var vehicleType = creationData.VehicleType;
            var rate = _parkingRatesProvider.GetRateByVehicleType(vehicleType);
            var totalMinutes = (int)creationData.StaysTimeRanges.Sum(timeRange => (timeRange.ExitTime.Subtract(timeRange.EntryTime).TotalMinutes));

            return new Invoice()
            {
                LicensePlase = creationData.LicensePlate,
                TotalTimeInMinutes = totalMinutes,
                TotalAmountToPay = totalMinutes * rate
            };
        }

        public Task<IEnumerable<Invoice>> GeneratePaymentsForResidentsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
