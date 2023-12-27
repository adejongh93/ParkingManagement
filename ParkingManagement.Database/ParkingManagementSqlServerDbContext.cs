using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Database
{
    internal class ParkingManagementSqlServerDbContext : DbContext, IParkingManagementDbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleStay> VehiclesStay { get; set; }

        public async Task SaveChangesAsync()
            => await base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("connectionString");
        }
    }
}
