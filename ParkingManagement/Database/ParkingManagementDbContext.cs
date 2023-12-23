using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.Models;
using System.Threading.Tasks;

namespace ParkingManagement.Database
{
    internal class ParkingManagementDbContext : DbContext, IParkingManagementDbContext
    {
        public DbSet<VehicleDataModel> Vehicles { get; set; }

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
