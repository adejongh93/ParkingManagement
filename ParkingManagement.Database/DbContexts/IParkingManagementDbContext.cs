﻿using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Database.DbContexts
{
    internal interface IParkingManagementDbContext : IDisposable
    {
        DbSet<Vehicle> Vehicles { get; set; }

        DbSet<VehicleStay> VehiclesStay { get; set; }

        Task SaveChangesAsync();
    }
}
