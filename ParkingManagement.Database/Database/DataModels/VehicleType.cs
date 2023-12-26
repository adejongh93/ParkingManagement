using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ParkingManagement.Database.Database.DataModels
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VehicleType
    {
        EXTERNAL,
        OFFICIAL,
        RESIDENT,
    }
}
