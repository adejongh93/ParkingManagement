using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Repositories;

namespace ParkingManagement.Services.Services.VehicleStays
{
    internal class VehicleStaysService : IVehicleStaysService
    {
        private readonly IVehicleStayRepository vehicleStayRepository;

        public VehicleStaysService(IVehicleStayRepository vehicleStayRepository)
        {
            this.vehicleStayRepository = vehicleStayRepository;
        }

        public async Task AddyAsync(VehicleStay vehicleStay)
            => await vehicleStayRepository.AddAsync(vehicleStay);

        public async Task UpdateAsync(VehicleStay vehicleStay)
            => await vehicleStayRepository.UpdateAsync(vehicleStay);

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

        public VehicleStay GetVehicleNotCompletedStay(string licensePlate)
            => vehicleStayRepository.GetVehicleNotCompletedStay(licensePlate);
    }
}
