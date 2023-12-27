using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Repositories;

namespace ParkingManagement.Providers.VehicleStaysProvider
{
    internal class VehicleStaysProvider : IVehicleStaysProvider
    {
        private readonly IVehicleStayRepository vehicleStayRepository;

        public VehicleStaysProvider(IVehicleStayRepository vehicleStayRepository)
        {
            this.vehicleStayRepository = vehicleStayRepository;
        }

        public async Task<IEnumerable<VehicleStay>> GetAllAsync()
            => await vehicleStayRepository.GetAllAsync();

        public async Task<int> CountAsync()
            => await vehicleStayRepository.CountAsync();

        public IEnumerable<VehicleStay> GetNotCompletedStays()
            => vehicleStayRepository.GetNotCompletedStays();

        public IEnumerable<VehicleStay> GetCompletedStays()
            => vehicleStayRepository.GetCompletedStays();
    }
}
