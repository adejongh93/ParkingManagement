using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Database.Repositories
{
    public class VehiclesInParkingRepository : IVehiclesInParkingRepository
    {
        private readonly IParkingManagementDbContext dbContext;

        public VehiclesInParkingRepository(IParkingManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public async Task<IEnumerable<VehicleInParking>> GetAllAsync()
        {
            return await dbContext.VehiclesInParking.ToListAsync();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(VehicleInParking vehicle)
        {
            await dbContext.VehiclesInParking.AddAsync(vehicle); // I don't think we need to worry a lot about concurrency in this application
            await dbContext.SaveChangesAsync();
        }

        public async Task<VehicleInParking> GetAsync(string id)
            => await dbContext.VehiclesInParking.FindAsync(id);

        public async Task<bool> ExistsAsync(string id)
            => await GetAsync(id) is not null;

        public async Task RemoveAsync(VehicleInParking vehicle)
        {
            dbContext.VehiclesInParking.Remove(vehicle); // I don't think we need to worry a lot about concurrency in this application
            await dbContext.SaveChangesAsync();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRangeAsync(IEnumerable<VehicleInParking> entities)
        {
            dbContext.VehiclesInParking.UpdateRange(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}