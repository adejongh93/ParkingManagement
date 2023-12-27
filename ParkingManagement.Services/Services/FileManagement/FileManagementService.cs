using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Services.Services.FileManagement
{
    internal class FileManagementService : IFileManagementService
    {
        public FileContentResult DownloadResidentsPayments(string fileName, IEnumerable<StayInvoice> invoices)
            => GenerateFileContentResult(fileName, invoices);

        private FileContentResult GenerateFileContentResult(string fileName, IEnumerable<StayInvoice> invoices)
        {
            fileName = string.IsNullOrEmpty(fileName) ? "sample.xlsx" : $"{fileName}.xlsx";

            byte[] excelData = GenerateExcelData(invoices);

            var response = new FileContentResult(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = fileName
            };

            return response;
        }

        private byte[] GenerateExcelData(IEnumerable<StayInvoice> invoices)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                worksheet.Cells["A1"].Value = "License Plate";
                worksheet.Cells["B1"].Value = "Total Time in Parking (mins.)";
                worksheet.Cells["C1"].Value = "Total Amount to Pay";

                var invoicesList = invoices.ToList();

                var count = invoicesList.Count();

                foreach (var idx in Enumerable.Range(0, count))
                {
                    worksheet.Cells[$"A{idx + 2}"].Value = invoicesList[idx].LicensePlase;
                    worksheet.Cells[$"B{idx + 2}"].Value = invoicesList[idx].TotalTimeInMinutes;
                    worksheet.Cells[$"C{idx + 2}"].Value = invoicesList[idx].TotalAmountToPay;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);

                return stream.ToArray();
            }
        }
    }
}
