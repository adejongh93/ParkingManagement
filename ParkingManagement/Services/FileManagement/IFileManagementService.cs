using Microsoft.AspNetCore.Mvc;
using ParkingManagement.DataModels;
using System.Collections.Generic;

namespace ParkingManagement.Services.FileManagement
{
    public interface IFileManagementService
    {
        FileContentResult DownloadResidentsPayments(string fileName, IEnumerable<StayInvoice> invoices);
    }
}
