using ParkingManagement.Database.Models;

namespace ParkingManagement.Repositories
{
    internal interface IVehicleRepository : IRepository<VehicleDataModel> // TODO: Check all datamodels for the API, the repository and the DB
    {
        //// For the future, maybe retrieve something specific to a vehicle like the owner and/or regular choffer
        // Task<Person> GetOwnerAsync(int licensePlate);
    }
}
