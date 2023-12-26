using System.Text.RegularExpressions;

namespace ParkingManagement.Services.Validations
{
    public class ValidationsService : IValidationsService
    {
        public bool IsValidLicensePlate(string licensePlate)
        {
            return Regex.IsMatch(licensePlate, @"^[A-Z]{4}-\d{4}$");
        }
    }
}
