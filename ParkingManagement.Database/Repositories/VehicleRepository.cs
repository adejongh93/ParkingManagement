using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Database.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly IParkingManagementDbContext dbContext;

        public VehicleRepository(IParkingManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(Vehicle newVehicle)
        {
            await dbContext.Vehicles.AddAsync(newVehicle); // I don't think we need to worry a lot about concurrency in this application
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await dbContext.Vehicles.ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await dbContext.Vehicles.CountAsync();
        }

        public async Task<Vehicle> GetAsync(string id)
            => await dbContext.Vehicles.FindAsync(id); // Check null here

        public async Task<bool> ExistsAsync(string licensePlate)
            => await GetAsync(licensePlate) is not null;

        public async Task ClearAsync()
        {
            dbContext.Vehicles.RemoveRange(dbContext.Vehicles);
            await dbContext.SaveChangesAsync();
        }

        public Task UpdateRangeAsync(IEnumerable<Vehicle> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Vehicle entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }
    }
}
