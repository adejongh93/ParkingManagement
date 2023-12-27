using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Database.Repositories
{
    public interface IVehicleStayRepository : IRepository<VehicleStay>
    {
        IEnumerable<VehicleStay> GetNotCompletedStays();

        IEnumerable<VehicleStay> GetCompletedStays();

        IEnumerable<VehicleStay> GetStaysByLicensePlate(string licensePlate);

        VehicleStay GetVehicleNotCompletedStay(string licensePlate);
    }
}
