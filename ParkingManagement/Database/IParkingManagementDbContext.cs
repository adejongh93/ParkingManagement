using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.Models;
using System;
using System.Threading.Tasks;

namespace ParkingManagement.Database
{
    internal interface IParkingManagementDbContext : IDisposable
    {
        DbSet<VehicleDataModel> Vehicles { get; set; }

        Task SaveChangesAsync();
    }
}
