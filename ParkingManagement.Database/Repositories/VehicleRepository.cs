using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database;
using ParkingManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.Repositories
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
            => await dbContext.Vehicles.FindAsync(id);

        public async Task<bool> ExistsAsync(string id)
            => await GetAsync(id) is not null;

        public Task RemoveAsync(Vehicle entity)
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<Vehicle> entities)
        {
            throw new NotImplementedException();
        }
    }
}
