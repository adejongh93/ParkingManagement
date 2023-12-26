using ParkingManagement.Services.DataModels;
using ParkingManagement.Services.Services.Invoice.Models;

namespace ParkingManagement.Services.Services.Invoice
{
    public interface IInvoiceService
    {
        Task<DetailedInvoice> GenerateDetailedInvoiceAsync();

        StayInvoice GenerateInvoiceIfApplicable(InvoiceRequestData creationData);

        Task<IEnumerable<StayInvoice>> GenerateInvoicesForResidentsAsync();
    }
}
