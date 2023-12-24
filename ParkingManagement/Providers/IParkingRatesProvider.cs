using ParkingManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Providers
{
    public interface IParkingRatesProvider
    {
        double GetRateByVehicleType(VehicleType vehicleType);
    }
}
