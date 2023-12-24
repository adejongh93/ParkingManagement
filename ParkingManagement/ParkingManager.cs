using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using ParkingManagement.DataModels;
using ParkingManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManagement
{
    internal class ParkingManager : IParkingManager
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IVehicleInParkingRepository vehiclesInParkingRepository;
        private readonly IVehicleStayRepository vehicleStayRepository;

        public ParkingManager(IVehicleRepository vehicleRepository,
            IVehicleInParkingRepository vehicleInParkingRepository,
            IVehicleStayRepository vehicleStayRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehiclesInParkingRepository = vehicleInParkingRepository;
            this.vehicleStayRepository = vehicleStayRepository;
        }

        public async Task<IEnumerable<Payment>> GenerateResidentsPaymentsAsync()
        {
            var residentsLicensePlates = (await vehicleRepository.GetAllAsync())
                .Where(vehicle => vehicle.Type == VehicleType.Resident)
                .Select(vehicle => vehicle.LicensePlate).ToList();

            var stays = (await vehicleStayRepository.GetAllAsync()).ToList();
            var residentStays = stays.Where(stay => residentsLicensePlates.Contains(stay.LicensePlate));

            var vehiclesInParking = (await vehiclesInParkingRepository.GetAllAsync()).ToList();
            var residentsInParking = vehiclesInParking.Where(vehicle => residentsLicensePlates.Contains(vehicle.LicensePlate)).ToList();

            residentStays = residentStays.Union(residentsInParking.Select(vehicle => new VehicleStay()
            {
                LicensePlate = vehicle.LicensePlate,
                EntryTime = vehicle.EntryTime,
                ExitTime = DateTime.UtcNow
            }));

            var groups = residentStays.GroupBy(vehicle => vehicle.LicensePlate);

            return groups.Select(group =>
            {
                var timeInParking = (int)group.Sum(stay => stay.ExitTime.Subtract(stay.EntryTime).TotalMinutes);
                var totalToPay = Math.Round(timeInParking * 0.05, 2);
                return new Payment()
                {
                    LicensePlate = group.Key,
                    TimeInParking = timeInParking,
                    TotalToPay = totalToPay
                };
            });
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await vehicleRepository.GetAllAsync();
        }

        public async Task<int> GetVehiclesCountAsync()
        {
            return await vehicleRepository.GetCountAsync();
        }

        public async Task RegisterEntryAsync(string licensePlate)
        {
            if (!await vehicleRepository.ExistsAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is not registered in the system.");
            }

            if (await vehiclesInParkingRepository.ExistsAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is already in the parking.");
            }

            await vehiclesInParkingRepository.AddAsync(new VehicleInParking()
            {
                LicensePlate = licensePlate,
                EntryTime = DateTime.UtcNow
            });
        }

        public async Task RegisterExitAsync(string licensePlate)
        {
            if (!await vehicleRepository.ExistsAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is not registered in the system.");
            }

            if (!await vehiclesInParkingRepository.ExistsAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is not in the parking.");
            }

            var vehicle = await vehiclesInParkingRepository.GetAsync(licensePlate);

            await vehicleStayRepository.AddAsync(new VehicleStay()
            {
                LicensePlate = licensePlate,
                EntryTime = vehicle.EntryTime,
                ExitTime = DateTime.UtcNow
            });

            await vehiclesInParkingRepository.RemoveAsync(vehicle);
        }

        public async Task RegisterOfficialVehicleAsync(string licensePlate)
            => await RegisterVehicleAsync(licensePlate, VehicleType.Official);

        public async Task RegisterResidentVehicleAsync(string licensePlate)
            => await RegisterVehicleAsync(licensePlate, VehicleType.Resident);

        public Task ResetAsync()
        {
            throw new NotImplementedException();
        }

        private async Task RegisterVehicleAsync(string licensePlate, VehicleType vehicleType)
        {
            if (await vehicleRepository.ExistsAsync(licensePlate))
            {
                throw new InvalidOperationException($"Vehicle with license plate {licensePlate} is already registered in the system.");
            }

            await vehicleRepository.AddAsync(new Vehicle()
            {
                LicensePlate = licensePlate,
                Type = vehicleType
            });
        }
    }
}
