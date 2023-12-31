﻿using ParkingManagement.CommonLibrary;
using ParkingManagement.Database.Repositories;
using ParkingManagement.Services.DataModels;
using ParkingManagement.Services.Mappers;

namespace ParkingManagement.Services.Providers.VehiclesProvider
{
    internal class VehiclesProvider : IVehiclesProvider
    {
        private readonly IVehicleRepository vehicleRepository;

        private readonly IVehicleMapper vehicleMapper;

        public VehiclesProvider(IVehicleRepository vehicleRepository,
            IVehicleMapper vehicleMapper)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehicleMapper = vehicleMapper;
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
            => (await vehicleRepository.GetAllAsync())
            .Select(vehicle => vehicleMapper.Map(vehicle));

        public async Task<VehicleDto?> FindAsync(string licensePlate)
            => (await vehicleRepository.FindAsync(licensePlate)) is var vehicleDto && 
            vehicleDto is not null ? vehicleMapper.Map(vehicleDto) : null;

        public async Task<int> CountAsync()
            => await vehicleRepository.CountAsync();

        public async Task<VehicleType?> GetVehicleTypeAsync(string licensePlate)
            => (await FindAsync(licensePlate))?.Type;

        public IEnumerable<string> GetAllLicensePlatesByVehicleType(VehicleType vehicleType)
            => vehicleRepository.GetAllByVehicleType(vehicleType).Select(vehicle => vehicleMapper.Map(vehicle).LicensePlate);
    }
}
