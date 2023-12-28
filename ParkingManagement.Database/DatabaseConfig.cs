namespace ParkingManagement.Database
{
    public class DatabaseConfig
    {
        public string ConnectionString { get; set; }
        
        public DatabaseProvider Provider { get; set; }
    }

    public enum DatabaseProvider
    {
        InMemory,
        SqlServer
    }
}
