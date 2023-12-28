using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using ParkingManagement.Database;

[assembly: FunctionsStartup(typeof(ParkingManagement.Startup))]

namespace ParkingManagement
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            Services.ServicesConfig.ConfigureServices(builder);
            ServicesConfig.ConfigureServices(builder);
        }
    }
}
