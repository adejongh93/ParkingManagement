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

        public async Task<bool> TryAddAsync(VehicleDataModel newVehicle)
        {
            if (await VehicleExists(newVehicle.LicensePlate))
            {
                return false;
            }

            await _dbContext.Vehicles.AddAsync(newVehicle); // I don't think we need to worry a lot about concurrency in this application
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<IEnumerable<VehicleDataModel>> GetAllAsync()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _dbContext.Vehicles.CountAsync();
        }

        public Task<VehicleDataModel> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> VehicleExists(string licensePlate)
            => await _dbContext.Vehicles.AnyAsync(vehicle => vehicle.LicensePlate == licensePlate);
    }
}
