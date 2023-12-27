using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Providers.VehicleStaysProvider
{
    public interface IVehicleStaysProvider
    {
        Task<IEnumerable<VehicleStay>> GetAllVehicleStaysAsync();

        Task<int> GetVehicleStaysCountAsync();

        IEnumerable<VehicleStay> GetNotCompletedStays();

        IEnumerable<VehicleStay> GetCompletedStays();
    }
}
