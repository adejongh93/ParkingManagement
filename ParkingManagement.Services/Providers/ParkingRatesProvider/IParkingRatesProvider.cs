﻿using ParkingManagement.CommonLibrary;

namespace ParkingManagement.Services.Providers.ParkingRatesProvider
{
    public interface IParkingRatesProvider
    {
        double GetRateByVehicleType(VehicleType vehicleType);

        bool PaysOnExit(VehicleType vehicleType);
    }
}
