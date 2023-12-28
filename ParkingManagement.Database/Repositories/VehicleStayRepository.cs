using Microsoft.EntityFrameworkCore;
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

        public async Task<VehicleStay?> FindAsync(string id)
            => await dbContext.VehiclesStay.FindAsync(id);

        public IEnumerable<VehicleStay> GetCompletedStays()
            => dbContext.VehiclesStay.Where(stay => stay.ExitTime != null);

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VehicleStay> GetNotCompletedStays()
            => dbContext.VehiclesStay.Where(stay => stay.ExitTime == null);

        public IEnumerable<VehicleStay> GetStaysByLicensePlate(string licensePlate)
            => dbContext.VehiclesStay.Where(stay => stay.LicensePlate == licensePlate);

        public VehicleStay? GetVehicleNotCompletedStay(string licensePlate)
            => dbContext.VehiclesStay
                .FirstOrDefault(stay => stay.LicensePlate == licensePlate && stay.ExitTime == null);

        public async Task RemoveRangeAsync(IEnumerable<string> ids)
        {
            var staysToRemove = dbContext.VehiclesStay.Where(stay => ids.Contains(stay.Id));
            dbContext.VehiclesStay.RemoveRange(staysToRemove);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(VehicleStay vehicleStay)
        {
            var stay = await dbContext.VehiclesStay.FindAsync(vehicleStay.Id);

            if (stay is null)
            {
                throw new Exception($"Could not find stay with Id {vehicleStay.Id} to be updated");
            }

            stay.EntryTime = vehicleStay.EntryTime;
            stay.ExitTime = vehicleStay.ExitTime;
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
