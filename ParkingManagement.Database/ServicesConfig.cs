using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingManagement.Database.DbContexts;
using ParkingManagement.Database.Repositories;

namespace ParkingManagement.Database
{
    public static class ServicesConfig
    {
        public static void ConfigureServices(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
            builder.Services.AddSingleton<IVehicleStayRepository, VehicleStayRepository>();

            var config = builder.GetContext().Configuration;
            var dbConfig = config.GetSection("DatabaseConfig").Get<DatabaseConfig>();

            switch (dbConfig.Provider)
            {
                case DatabaseProvider.InMemory:
                    builder.Services.AddDbContext<IParkingManagementDbContext, ParkingManagementInMemoryDbContext>
                        (ServiceLifetime.Singleton, ServiceLifetime.Singleton);
                    break;
                case DatabaseProvider.SqlServer:
                    builder.Services.AddDbContext<IParkingManagementDbContext, ParkingManagementSqlServerDbContext>(
                        options => options.UseSqlServer(dbConfig.ConnectionString), 
                        ServiceLifetime.Singleton, ServiceLifetime.Singleton);
                    break;
            }
        }
    }
}
