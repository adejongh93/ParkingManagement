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

            var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();

            var configuration = configBuilder.Build();

            Enum.TryParse<DatabaseProvider>(configuration["DatabaseProvider"], out var dbProvider);

            var dbConfig = new DatabaseConfig()
            {
                Provider = dbProvider,
                ConnectionString = configuration["DbConnectionString"]
            };

            switch (dbConfig.Provider)
            {
                case DatabaseProvider.InMemory:
                    builder.Services.AddDbContext<IParkingManagementDbContext, ParkingManagementInMemoryDbContext>
                        (ServiceLifetime.Singleton, ServiceLifetime.Singleton);
                    break;
                case DatabaseProvider.SqlServer:
                    if (dbConfig.ConnectionString is null)
                    {
                        throw new Exception("Database Connection String cannot be null when using SQL Server Provider");
                    }

                    builder.Services.AddDbContext<IParkingManagementDbContext, ParkingManagementSqlServerDbContext>(
                        options => options.UseSqlServer(dbConfig.ConnectionString),
                        ServiceLifetime.Singleton, ServiceLifetime.Singleton);
                    break;
            }
        }
    }
}
