using ParkingManagement.Database.DataModels;
using ParkingManagement.Repositories;
using System.Threading.Tasks;

namespace ParkingManagement.Services.VehicleStays
{
    public class VehicleStaysService : IVehicleStaysService
    {
        private readonly IVehicleStayRepository vehicleStayRepository;

        public VehicleStaysService(IVehicleStayRepository vehicleStayRepository)
        {
            this.vehicleStayRepository = vehicleStayRepository;
        }

        public async Task AddVehicleStayAsync(VehicleStay vehicleStay)
        {
            await vehicleStayRepository.AddAsync(vehicleStay);
        }

        public async Task ClearAllVehicleStaysAsync()
        {
            await vehicleStayRepository.ClearAsync();
        }
    }
}
