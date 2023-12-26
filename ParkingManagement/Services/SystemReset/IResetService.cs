using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingManagement.Services.SystemReset
{
    public interface IResetService
    {
        Task ExecutePartialResetAsync();

        Task ExecuteFullReset();
    }
}
