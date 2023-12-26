using ParkingManagement.Database.Database.DataModels;
using ParkingManagement.Database.Repositories;

namespace ParkingManagement.Providers.VehicleStaysProvider
{
    public class VehicleStaysProvider : IVehicleStaysProvider
    {
        private readonly IVehicleStayRepository vehicleStayRepository;

        public VehicleStaysProvider(IVehicleStayRepository vehicleStayRepository)
        {
            this.vehicleStayRepository = vehicleStayRepository;
        }

        public async Task<IEnumerable<VehicleStay>> GetAllVehicleStaysAsync()
        {
            return await vehicleStayRepository.GetAllAsync();
        }

        public async Task<VehicleStay> GetVehicleStayAsync(string licensePlate)
        {
            return await vehicleStayRepository.GetAsync(licensePlate);
        }

        public async Task<int> GetVehicleStaysCountAsync()
        {
            return await vehicleStayRepository.GetCountAsync();
        }
    }
}
