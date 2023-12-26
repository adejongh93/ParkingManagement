using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.Database.DataModels;

namespace ParkingManagement.Database.Database
{
    public class ParkingManagementDbContext : DbContext, IParkingManagementDbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleInParking> VehiclesInParking { get; set; }

        public DbSet<VehicleStay> VehiclesStay { get; set; }

        public async Task SaveChangesAsync()
        {
            await (this as DbContext).SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ParkingManagement");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
