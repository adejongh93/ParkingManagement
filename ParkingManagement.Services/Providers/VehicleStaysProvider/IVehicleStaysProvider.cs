using ParkingManagement.Database.DataModels;
using ParkingManagement.Services.DataModels;

namespace ParkingManagement.Providers.VehicleStaysProvider
{
    public interface IVehicleStaysProvider
    {
        Task<IEnumerable<VehicleStay>> GetAllAsync();

        Task<int> CountAsync();

        IEnumerable<VehicleStay> GetNotCompletedStays();

        IEnumerable<VehicleStay> GetCompletedStays();

        int GetNotCompletedStaysCount();
    }
}
