using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace ParkingManagement.Database.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VehicleType
    {
        EXTERNAL,
        OFFICIAL,
        RESIDENT    ,
    }
}
