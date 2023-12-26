namespace ParkingManagement.Services.Services.SystemReset
{
    public interface IResetService
    {
        Task ExecutePartialResetAsync();

        Task ExecuteFullResetAsync();
    }
}
