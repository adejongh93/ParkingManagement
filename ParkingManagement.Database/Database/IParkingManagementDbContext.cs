using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using System;
using System.Threading.Tasks;

namespace ParkingManagement.Database
{
    public interface IParkingManagementDbContext : IDisposable
    {
        DbSet<Vehicle> Vehicles { get; set; }

        DbSet<VehicleInParking> VehiclesInParking { get; set; }

        DbSet<VehicleStay> VehiclesStay { get; set; }

        Task SaveChangesAsync();
    }
}
