using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ParkingManagement.Common
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VehicleType
    {
        EXTERNAL,
        OFFICIAL,
        RESIDENT,
    }
}
