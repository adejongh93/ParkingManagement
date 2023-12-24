using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database;
using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Repositories
{
    internal class VehicleInParkingRepository : IVehicleInParkingRepository
    {
        private readonly IParkingManagementDbContext _dbContext;

        public VehicleInParkingRepository(IParkingManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VehicleInParking>> GetAllAsync()
        {
            return await _dbContext.VehiclesInParking.ToListAsync();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(VehicleInParking vehicle)
        {
            await _dbContext.VehiclesInParking.AddAsync(vehicle); // I don't think we need to worry a lot about concurrency in this application
            await _dbContext.SaveChangesAsync();
        }

        public async Task<VehicleInParking> GetAsync(string id)
            => await _dbContext.VehiclesInParking.FindAsync(id);

        public async Task<bool> ExistsAsync(string id)
            => await GetAsync(id) is not null;

        public async Task RemoveAsync(VehicleInParking vehicle)
        {
            _dbContext.VehiclesInParking.Remove(vehicle); // I don't think we need to worry a lot about concurrency in this application
            await _dbContext.SaveChangesAsync();
        }
    }
}