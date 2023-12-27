using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Database.Repositories
{
    public interface IVehicleRepository : IRepository<Vehicle> // TODO: Check all datamodels for the API, the repository and the DB
    {
        //// For the future, maybe retrieve something specific to a vehicle like the owner and/or regular choffer
        // Task<Person> GetOwnerAsync(int licensePlate);

        Task<bool> ExistsByLicensePlateAsync(string licensePlate);
    }
}
