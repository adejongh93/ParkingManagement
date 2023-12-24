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
        private readonly IParkingManagementDbContext _dbContext;

        public VehicleStayRepository(IParkingManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(VehicleStay vehicle)
        {
            await _dbContext.VehiclesStay.AddAsync(vehicle); // I don't think we need to worry a lot about concurrency in this application
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VehicleStay>> GetAllAsync()
        {
            return await _dbContext.VehiclesStay.ToListAsync();
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
    }
}
