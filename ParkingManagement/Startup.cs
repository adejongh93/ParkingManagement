using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ParkingManagement.Database;
using ParkingManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(ParkingManagement.Startup))]

namespace ParkingManagement
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<IParkingManagementDbContext, ParkingManagementDbContext>(ServiceLifetime.Singleton, ServiceLifetime.Singleton);
            builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>(); // TODO: Check if this can be scope
            builder.Services.AddSingleton<IParkingManager, ParkingManager>(); // TODO: Check if this can be scope
        }
    }
}
