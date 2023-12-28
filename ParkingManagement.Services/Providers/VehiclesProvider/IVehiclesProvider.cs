using ParkingManagement.CommonLibrary;
using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Services.Providers.VehiclesProvider
{
    public interface IVehiclesProvider
    {
        Task<VehicleDto> FindAsync(string licensePlate);

        Task<IEnumerable<VehicleDto>> GetAllAsync();

        Task<int> CountAsync();

        Task<VehicleType> GetVehicleTypeAsync(string licensePlate);

        IEnumerable<string> GetAllLicensePlatesByVehicleTypeAsync(VehicleType vehicleType);
    }
}
