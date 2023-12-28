using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ParkingManagement.Providers.VehicleStaysProvider;
using ParkingManagement.Services.Mappers;
using ParkingManagement.Services.Providers.ParkingRatesProvider;
using ParkingManagement.Services.Providers.VehiclesProvider;
using ParkingManagement.Services.Services.FileManagement;
using ParkingManagement.Services.Services.Invoice;
using ParkingManagement.Services.Services.ParkingAccess;
using ParkingManagement.Services.Services.SystemReset;
using ParkingManagement.Services.Services.Validations;
using ParkingManagement.Services.Services.VehicleRegistration;
using ParkingManagement.Services.Services.VehicleStays;

namespace ParkingManagement.Services
{
    public static class ServicesConfig
    {
        public static void ConfigureServices(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IParkingManager, ParkingManager>();

            builder.Services.AddSingleton<IVehiclesProvider, VehiclesProvider>();
            builder.Services.AddSingleton<IVehicleStaysProvider, VehicleStaysProvider>();
            builder.Services.AddSingleton<IParkingRatesProvider, ParkingRatesProvider>();

            builder.Services.AddSingleton<IInvoiceService, InvoiceService>();
            builder.Services.AddSingleton<IParkingAccessService, ParkingAccessService>();
            builder.Services.AddSingleton<IVehicleRegistrationService, VehicleRegistrationService>();
            builder.Services.AddSingleton<IVehicleStaysService, VehicleStaysService>();
            builder.Services.AddSingleton<IFileManagementService, FileManagementService>();
            builder.Services.AddSingleton<IValidationsService, ValidationsService>();
            builder.Services.AddSingleton<IResetService, ResetService>();

            builder.Services.AddSingleton<IVehicleMapper, VehicleMapper>();
            builder.Services.AddSingleton<IVehicleStayMapper, VehicleStayMapper>();
        }
    }
}
