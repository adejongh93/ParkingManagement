using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.Database.DataModels;

namespace ParkingManagement.Database.Database
{
    public interface IParkingManagementDbContext : IDisposable
    {
        DbSet<Vehicle> Vehicles { get; set; }

        DbSet<VehicleInParking> VehiclesInParking { get; set; }

        DbSet<VehicleStay> VehiclesStay { get; set; }

        Task SaveChangesAsync();
    }
}
