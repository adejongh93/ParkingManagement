using ParkingManagement.Database.Repositories;
using ParkingManagement.Services.DataModels;
using ParkingManagement.Services.Mappers;

namespace ParkingManagement.Providers.VehicleStaysProvider
{
    internal class VehicleStaysProvider : IVehicleStaysProvider
    {
        private readonly IVehicleStayRepository vehicleStayRepository;

        private readonly IVehicleStayMapper vehicleStayMapper;

        public VehicleStaysProvider(IVehicleStayRepository vehicleStayRepository,
            IVehicleStayMapper vehicleStayMapper)
        {
            this.vehicleStayRepository = vehicleStayRepository;
            this.vehicleStayMapper = vehicleStayMapper;
        }

        public async Task<IEnumerable<VehicleStayDto>> GetAllAsync()
            => (await vehicleStayRepository.GetAllAsync())
            .Select(stay => vehicleStayMapper.Map(stay));

        public async Task<int> CountAsync()
            => await vehicleStayRepository.CountAsync();

        public IEnumerable<VehicleStayDto> GetNotCompletedStays()
            => vehicleStayRepository.GetNotCompletedStays()
            .Select(stay => vehicleStayMapper.Map(stay));

        public IEnumerable<VehicleStayDto> GetCompletedStays()
            => vehicleStayRepository.GetCompletedStays()
            .Select(stay => vehicleStayMapper.Map(stay));

        public int GetNotCompletedStaysCount()
            => GetNotCompletedStays().Count();
    }
}
