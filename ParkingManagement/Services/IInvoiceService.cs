using ParkingManagement.Database.DataModels;
using ParkingManagement.DataModels;
using ParkingManagement.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Services
{
    public interface IInvoiceService
    {
        Task<DetailedInvoice> GenerateDetailedInvoiceAsync();

        Invoice GenerateInvoice(InvoiceCreationData creationData);

        Task<IEnumerable<Invoice>> GeneratePaymentsForResidentsAsync();
    }
}
