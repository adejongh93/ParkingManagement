using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Database.DbContexts
{
    internal class ParkingManagementSqlServerDbContext : DbContext, IParkingManagementDbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleStay> VehiclesStay { get; set; }

        public ParkingManagementSqlServerDbContext(DbContextOptions<ParkingManagementSqlServerDbContext> dbContextOptions) : base(dbContextOptions) { }

        public async Task SaveChangesAsync()
            => await base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .Property(e => e.Type)
                .HasColumnType("nvarchar(50)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
