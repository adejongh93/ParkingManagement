using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ParkingManagement.CommonLibrary
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VehicleType
    {
        EXTERNAL,
        OFFICIAL,
        RESIDENT,
    }
}
