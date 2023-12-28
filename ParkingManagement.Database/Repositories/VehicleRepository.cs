using Microsoft.EntityFrameworkCore;
using ParkingManagement.CommonLibrary;
using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Database.Repositories
{
    internal class VehicleRepository : IVehicleRepository
    {
        private readonly IParkingManagementDbContext dbContext;

        public VehicleRepository(IParkingManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Vehicle newVehicle)
        {
            await dbContext.Vehicles.AddAsync(newVehicle); // I don't think we need to worry a lot about concurrency in this application
            await SaveChangesAsync();
        }

        public void Dispose()
            => dbContext.Dispose();

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
            => await dbContext.Vehicles.ToListAsync();

        public async Task<int> CountAsync()
            => await dbContext.Vehicles.CountAsync();

        public async Task<Vehicle?> FindAsync(string licensePlate)
            => await dbContext.Vehicles.FindAsync(licensePlate);

        public async Task<bool> ExistsByLicensePlateAsync(string licensePlate)
            => await FindAsync(licensePlate) is not null;

        public async Task ClearAsync()
        {
            dbContext.Vehicles.RemoveRange(dbContext.Vehicles);
            await SaveChangesAsync();
        }

        public IEnumerable<Vehicle> GetAllByVehicleType(VehicleType vehicleType)
            => dbContext.Vehicles.AsEnumerable().Where(vehicle => vehicle.Type == vehicleType);

        public Task UpdateRangeAsync(IEnumerable<Vehicle> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Vehicle entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRangeAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        private async Task SaveChangesAsync()
            => await dbContext.SaveChangesAsync();
    }
}
