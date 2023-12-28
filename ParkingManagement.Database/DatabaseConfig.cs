namespace ParkingManagement.Database
{
    public class DatabaseConfig
    {
        public string ConnectionString { get; set; } = "Server=localhost;Database=ParkingManagement;Trusted_Connection=True;";

        public DatabaseProvider Provider { get; set; } = DatabaseProvider.InMemory;
    }

    public enum DatabaseProvider
    {
        InMemory,
        SqlServer
    }
}
