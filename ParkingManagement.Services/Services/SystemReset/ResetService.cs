using ParkingManagement.Database.Repositories;

namespace ParkingManagement.Services.Services.SystemReset
{
    public class ResetService : IResetService
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IVehiclesInParkingRepository vehiclesInParkingRepository;
        private readonly IVehicleStayRepository vehicleStayRepository;

        public ResetService(IVehicleRepository vehicleRepository,
            IVehiclesInParkingRepository vehiclesInParkingRepository,
            IVehicleStayRepository vehicleStayRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehiclesInParkingRepository = vehiclesInParkingRepository;
            this.vehicleStayRepository = vehicleStayRepository;
        }

        public async Task ExecuteFullResetAsync()
        {
            await vehicleStayRepository.ClearAsync();
            await vehiclesInParkingRepository.ClearAsync();
            await vehicleRepository.ClearAsync();
        }

        public async Task ExecutePartialResetAsync()
        {
            await vehicleStayRepository.ClearAsync();

            var vehiclesInParking = await vehiclesInParkingRepository.GetAllAsync();
            vehiclesInParking = vehiclesInParking.Select(vehicle =>
            {
                vehicle.EntryTime = DateTime.UtcNow;
                return vehicle;
            });

            await vehiclesInParkingRepository.UpdateRangeAsync(vehiclesInParking);
        }
    }
}
