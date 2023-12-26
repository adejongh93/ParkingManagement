using ParkingManagement.DataModels;
using ParkingManagement.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.Services.Invoice
{
    public interface IInvoiceService
    {
        Task<DetailedInvoice> GenerateDetailedInvoiceAsync();

        StayInvoice GenerateInvoiceIfApplicable(InvoiceRequestData creationData);

        Task<IEnumerable<StayInvoice>> GenerateInvoicesForResidentsAsync();
    }
}
