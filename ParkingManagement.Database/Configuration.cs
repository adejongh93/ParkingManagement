using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ParkingManagement.Database.Repositories;

namespace ParkingManagement.Database
{
    public static class Configuration
    {
        public static void ConfigureServices(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
            builder.Services.AddSingleton<IVehicleStayRepository, VehicleStayRepository>();

            builder.Services.AddDbContext<IParkingManagementDbContext, ParkingManagementInMemoryDbContext>
                (ServiceLifetime.Singleton, ServiceLifetime.Singleton);
        }
    }
}
