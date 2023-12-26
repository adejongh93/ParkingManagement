using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ParkingManagement.Database.Database;
using ParkingManagement.Database.Repositories;
using ParkingManagement.Providers.VehicleStaysProvider;
using ParkingManagement.Services;
using ParkingManagement.Services.Providers.ParkingRatesProvider;
using ParkingManagement.Services.Providers.VehiclesInParkingProvider;
using ParkingManagement.Services.Providers.VehiclesProvider;
using ParkingManagement.Services.Services.FileManagement;
using ParkingManagement.Services.Services.Invoice;
using ParkingManagement.Services.Services.ParkingAccess;
using ParkingManagement.Services.Services.Validations;
using ParkingManagement.Services.Services.VehicleRegistration;
using ParkingManagement.Services.Services.VehicleStays;

[assembly: FunctionsStartup(typeof(ParkingManagement.Startup))]

namespace ParkingManagement
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // TODO: Check if all this can be scoped
            builder.Services.AddSingleton<IParkingManager, ParkingManager>();

            builder.Services.AddSingleton<IValidationsService, ValidationsService>();

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
