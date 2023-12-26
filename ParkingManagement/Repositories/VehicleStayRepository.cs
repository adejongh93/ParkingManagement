using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database;
using ParkingManagement.Database.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Repositories
{
    internal class VehicleStayRepository : IVehicleStayRepository
    {
        private readonly IParkingManagementDbContext dbContext;

        public VehicleStayRepository(IParkingManagementDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(VehicleStay vehicle)
        {
            await dbContext.VehiclesStay.AddAsync(vehicle); // I don't think we need to worry a lot about concurrency in this application
            await dbContext.SaveChangesAsync();
        }

        public async Task ClearAsync()
        {
            dbContext.VehiclesStay.RemoveRange(dbContext.VehiclesStay);
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public Task<bool> ExistsAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VehicleStay>> GetAllAsync()
        {
            return await dbContext.VehiclesStay.ToListAsync();
        }

        public Task<VehicleStay> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(VehicleStay entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<VehicleStay> entities)
        {
            throw new NotImplementedException();
        }
    }
}
