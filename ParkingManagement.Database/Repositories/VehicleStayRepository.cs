﻿using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Database.Repositories
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
            await SaveChangesAsync();
        }

        public async Task ClearAsync()
        {
            dbContext.VehiclesStay.RemoveRange(dbContext.VehiclesStay);
            await SaveChangesAsync();
        }

        public void Dispose()
            => dbContext.Dispose();

        public async Task<IEnumerable<VehicleStay>> GetAllAsync()
            => await dbContext.VehiclesStay.ToListAsync();

        public async Task<VehicleStay> FindAsync(string id)
            => await dbContext.VehiclesStay.FindAsync(id); // TODO: Check null here

        public IEnumerable<VehicleStay> GetCompletedStays()
            => dbContext.VehiclesStay.AsEnumerable().Where(stay => stay.StayCompleted);

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleStay> GetNotCompletedStays()
            => dbContext.VehiclesStay.AsEnumerable().Where(stay => !stay.StayCompleted);

        public IEnumerable<VehicleStay> GetStaysByLicensePlate(string licensePlate)
            => dbContext.VehiclesStay.Where(stay => stay.LicensePlate == licensePlate);

        public VehicleStay GetVehicleNotCompletedStay(string licensePlate)
            => dbContext.VehiclesStay.AsEnumerable()
                .FirstOrDefault(stay => stay.LicensePlate == licensePlate && !stay.StayCompleted); // TODO: Check null here

        public async Task RemoveRangeAsync(IEnumerable<string> ids)
        {
            var staysToRemove = dbContext.VehiclesStay.Where(stay => ids.Contains(stay.Id));
            dbContext.VehiclesStay.RemoveRange(staysToRemove);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(VehicleStay vehicleStay)
        {
            var stay = await dbContext.VehiclesStay.FindAsync(vehicleStay.Id);
            stay.EntryTime = vehicleStay.EntryTime; // TODO: Check null and key conflict here
            stay.ExitTime = vehicleStay.ExitTime; // TODO: Check null and key conflict here
            dbContext.VehiclesStay.Update(stay); // I don't think we need to worry a lot about concurrency in this application
            await SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<VehicleStay> entities)
        {
            dbContext.VehiclesStay.UpdateRange(entities);
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
            => await dbContext.SaveChangesAsync();
    }
}
