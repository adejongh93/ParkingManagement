using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database;
using ParkingManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingManagement.Repositories
{
    internal class VehicleRepository : IVehicleRepository
    {
        private readonly IParkingManagementDbContext _dbContext;

        public VehicleRepository(IParkingManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Vehicle newVehicle)
        {
            await _dbContext.Vehicles.AddAsync(newVehicle); // I don't think we need to worry a lot about concurrency in this application
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Vehicles.CountAsync();
        }

        public async Task<Vehicle> GetAsync(string id)
            => await _dbContext.Vehicles.FindAsync(id);

        public async Task<bool> ExistsAsync(string id)
            => await GetAsync(id) is not null;

        public Task RemoveAsync(Vehicle entity)
        {
            throw new NotImplementedException();
        }
    }
}
