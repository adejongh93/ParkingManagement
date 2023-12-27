using ParkingManagement.Database.Repositories;
using ParkingManagement.Services.Services.VehicleStays;

namespace ParkingManagement.Services.Services.SystemReset
{
    public class ResetService : IResetService
    {
        private readonly IVehicleStaysService vehicleStaysService;

        private readonly IVehicleRepository vehicleRepository;
        private readonly IVehicleStayRepository vehicleStayRepository;

        public ResetService(IVehicleStaysService vehicleStaysService,
            IVehicleRepository vehicleRepository,
            IVehicleStayRepository vehicleStayRepository)
        {
            this.vehicleStaysService = vehicleStaysService;

            this.vehicleRepository = vehicleRepository;
            this.vehicleStayRepository = vehicleStayRepository;
        }

        public async Task ExecuteFullResetAsync()
        {
            await vehicleStayRepository.ClearAsync();
            await vehicleRepository.ClearAsync();
        }

        public async Task ExecutePartialResetAsync()
        {
            await vehicleStaysService.DeleteAllCompletedStays();
            await vehicleStaysService.ResetEntryTimeForNotCompletedStaysAsync();
        }
    }
}
