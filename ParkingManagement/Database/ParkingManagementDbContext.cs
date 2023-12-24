using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using System.Threading.Tasks;

namespace ParkingManagement.Database
{
    internal class ParkingManagementDbContext : DbContext, IParkingManagementDbContext
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
        }
    }
}
