using ParkingManagement.Database.Repositories;
using ParkingManagement.Services.DataModels;
using ParkingManagement.Services.Mappers;

namespace ParkingManagement.Services.Services.VehicleStays
{
    internal class VehicleStaysService : IVehicleStaysService
    {
        private readonly IVehicleStayRepository vehicleStayRepository;

        private readonly IVehicleStayMapper vehicleStayMapper;

        public VehicleStaysService(IVehicleStayRepository vehicleStayRepository,
            IVehicleStayMapper vehicleStayMapper)
        {
            this.vehicleStayRepository = vehicleStayRepository;
            this.vehicleStayMapper = vehicleStayMapper;
        }

        public async Task AddyAsync(VehicleStayDto vehicleStay)
            => await vehicleStayRepository.AddAsync(vehicleStayMapper.Map(vehicleStay));

        public async Task UpdateAsync(VehicleStayDto vehicleStay)
            => await vehicleStayRepository.UpdateAsync(vehicleStayMapper.Map(vehicleStay));

        public async Task ClearAsync()
            => await vehicleStayRepository.ClearAsync();

        public async Task DeleteAllCompletedStays()
        {
            var completedStaysIds = vehicleStayRepository.GetCompletedStays()
                .Select(stay => stay.Id);

            await vehicleStayRepository.RemoveRangeAsync(completedStaysIds);
        }

        public async Task ResetEntryTimeForNotCompletedStaysAsync()
        {
            var notCompletedStays = vehicleStayRepository.GetNotCompletedStays()
                .Select(stay =>
                {
                    stay.EntryTime = DateTime.UtcNow;
                    return stay;
                });

            await vehicleStayRepository.UpdateRangeAsync(notCompletedStays);
        }

        public bool IsVehicleInParking(string licensePlate)
            => vehicleStayRepository.GetStaysByLicensePlate(licensePlate) // the vehicle is in the parking if and only if EXACTLY ONE of its stays is not completed
                .Where(stay => !stay.StayCompleted)
                .Count() == 1; // TODO: Improve this

        public VehicleStayDto? GetVehicleNotCompletedStay(string licensePlate)
            => vehicleStayRepository.GetVehicleNotCompletedStay(licensePlate) is var vehicleStay &&
            vehicleStay is not null ? vehicleStayMapper.Map(vehicleStay) : null;
    }
}
