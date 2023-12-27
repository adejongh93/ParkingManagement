using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Providers.VehicleStaysProvider
{
    public interface IVehicleStaysProvider
    {
        Task<VehicleStay> GetVehicleStayAsync(string licensePlate);

        Task<IEnumerable<VehicleStay>> GetAllVehicleStaysAsync();

        Task<int> GetVehicleStaysCountAsync();
    }
}
