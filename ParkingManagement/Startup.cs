using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ParkingManagement.Database;
using ParkingManagement.Providers.ParkingRatesProvider;
using ParkingManagement.Providers.VehicleProvider;
using ParkingManagement.Providers.VehiclesInParkingProvider;
using ParkingManagement.Providers.VehicleStaysProvider;
using ParkingManagement.Repositories;
using ParkingManagement.Services.FileManagement;
using ParkingManagement.Services.Invoice;
using ParkingManagement.Services.ParkingAccess;
using ParkingManagement.Services.VehicleRegistration;
using ParkingManagement.Services.VehicleStays;

[assembly: FunctionsStartup(typeof(ParkingManagement.Startup))]

namespace ParkingManagement
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // TODO: Check if all this can be scoped
            builder.Services.AddSingleton<IParkingManager, ParkingManager>();

            builder.Services.AddSingleton<IVehiclesProvider, VehiclesProvider>();
            builder.Services.AddSingleton<IVehiclesInParkingProvider, VehiclesInParkingProvider>();
            builder.Services.AddSingleton<IVehicleStaysProvider, VehicleStaysProvider>();
            builder.Services.AddSingleton<IParkingRatesProvider, ParkingRatesProvider>();

            builder.Services.AddSingleton<IInvoiceService, InvoiceService>();
            builder.Services.AddSingleton<IParkingAccessService, ParkingAccessService>();
            builder.Services.AddSingleton<IVehicleRegistrationService, VehicleRegistrationService>();
            builder.Services.AddSingleton<IVehicleStaysService, VehicleStaysService>();
            builder.Services.AddSingleton<IFileManagementService, FileManagementService>();

            builder.Services.AddSingleton<IVehicleRepository, VehicleRepository>();
            builder.Services.AddSingleton<IVehiclesInParkingRepository, VehiclesInParkingRepository>();
            builder.Services.AddSingleton<IVehicleStayRepository, VehicleStayRepository>();

            builder.Services.AddDbContext<IParkingManagementDbContext, ParkingManagementDbContext>(ServiceLifetime.Singleton, ServiceLifetime.Singleton);
        }
    }
}
