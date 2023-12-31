﻿using Microsoft.EntityFrameworkCore;
using ParkingManagement.Database.DataModels;

namespace ParkingManagement.Database.DbContexts
{
    internal class ParkingManagementInMemoryDbContext : DbContext, IParkingManagementDbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleStay> VehiclesStay { get; set; }

        public async Task SaveChangesAsync()
            => await base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ParkingManagement");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
