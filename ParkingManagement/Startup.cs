using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(ParkingManagement.Startup))]

namespace ParkingManagement
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Services.ServicesConfig.ConfigureServices(builder);
            Database.ServicesConfig.ConfigureServices(builder);
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            //builder.ConfigurationBuilder
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("dbConfig.json") // TODO: Fix this to work on Azure
            //    .Build();
        }
    }
}
