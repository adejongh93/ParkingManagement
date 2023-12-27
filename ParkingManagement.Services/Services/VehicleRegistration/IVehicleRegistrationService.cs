using ParkingManagement.CommonLibrary;

namespace ParkingManagement.Services.Services.VehicleRegistration
{
    public interface IVehicleRegistrationService
    {
        Task RegisterVehicleInTheSystemAsync(string licensePlate, VehicleType vehicleType);

        Task<bool> IsVehicleRegisteredInTheSystemAsync(string licensePlate);
    }
}
