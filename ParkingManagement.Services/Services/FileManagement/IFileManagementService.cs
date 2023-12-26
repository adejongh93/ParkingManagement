using Microsoft.AspNetCore.Mvc;
using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Services.Services.FileManagement
{
    public interface IFileManagementService
    {
        FileContentResult DownloadResidentsPayments(string fileName, IEnumerable<StayInvoice> invoices);
    }
}
