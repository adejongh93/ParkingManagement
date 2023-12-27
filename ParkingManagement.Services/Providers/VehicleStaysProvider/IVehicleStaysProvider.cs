using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Providers.VehicleStaysProvider
{
    public interface IVehicleStaysProvider
    {
        Task<IEnumerable<VehicleStayDto>> GetAllAsync();

        Task<int> CountAsync();

        IEnumerable<VehicleStayDto> GetNotCompletedStays();

        IEnumerable<VehicleStayDto> GetCompletedStays();

        int GetNotCompletedStaysCount();
    }
}
