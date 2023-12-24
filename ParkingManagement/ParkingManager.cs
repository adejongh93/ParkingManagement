using ParkingManagement.Database.DataModels;
using ParkingManagement.Database.Models;
using ParkingManagement.DataModels;
using ParkingManagement.Repositories;
using ParkingManagement.Services;
using ParkingManagement.Services.Models;
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

        private readonly IInvoiceService invoiceService;

        public ParkingManager(IVehicleRepository vehicleRepository,
            IVehicleInParkingRepository vehicleInParkingRepository,
            IVehicleStayRepository vehicleStayRepository,
            IInvoiceService invoiceService)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehiclesInParkingRepository = vehicleInParkingRepository;
            this.vehicleStayRepository = vehicleStayRepository;

            this.invoiceService = invoiceService;
        }

        public async Task<IEnumerable<Invoice>> GenerateResidentsPaymentsAsync()
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
                TimeRange = new VehicleStayTimeRange()
                {
                    EntryTime = vehicle.EntryTime,
                    ExitTime = DateTime.UtcNow
                }
            }));

            var groups = residentStays.GroupBy(vehicle => vehicle.LicensePlate);

            return groups.Select(group => invoiceService.GenerateInvoice(new InvoiceCreationData()
            {
                LicensePlate = group.Key,
                VehicleType = VehicleType.Resident,
                StaysTimeRanges = group.Select(stay => new VehicleStayTimeRange()
                {
                    EntryTime = stay.TimeRange.EntryTime,
                    ExitTime = stay.TimeRange.ExitTime
                })
            }));
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
                // Vehicle is not registered in the system. It will be registered as Default
                await RegisterDefaultVehicleAsync(licensePlate);
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

        public async Task<Invoice> RegisterExitAsync(string licensePlate)
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

            var entryTime = vehicle.EntryTime;
            var exitTime = DateTime.UtcNow;

            var timeRange = new VehicleStayTimeRange()
            {
                EntryTime = entryTime,
                ExitTime = exitTime
            };

            await vehicleStayRepository.AddAsync(new VehicleStay()
            {
                LicensePlate = licensePlate,
                TimeRange = timeRange
            });

            await vehiclesInParkingRepository.RemoveAsync(vehicle);

            var vehicleType = (await vehicleRepository.GetAsync(licensePlate)).Type;

            if (vehicleType is VehicleType.Default)
            {
                return GenerateInvoice(licensePlate, vehicleType, timeRange);

            }
            return null;
        }

        public async Task RegisterOfficialVehicleAsync(string licensePlate)
            => await RegisterVehicleAsync(licensePlate, VehicleType.Official);

        public async Task RegisterResidentVehicleAsync(string licensePlate)
            => await RegisterVehicleAsync(licensePlate, VehicleType.Resident);

        public Task ResetAsync()
        {
            throw new NotImplementedException();
        }

        private async Task RegisterDefaultVehicleAsync(string licensePlate)
            => await RegisterVehicleAsync(licensePlate, VehicleType.Default);

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

        private Invoice GenerateInvoice(string licensePlate, VehicleType vehicleType, VehicleStayTimeRange timeRange)
        {
            return invoiceService.GenerateInvoice(new InvoiceCreationData()
            {
                LicensePlate = licensePlate,
                VehicleType = vehicleType,
                StaysTimeRanges = new List<VehicleStayTimeRange>() { timeRange }
            });
        }
    }
}
