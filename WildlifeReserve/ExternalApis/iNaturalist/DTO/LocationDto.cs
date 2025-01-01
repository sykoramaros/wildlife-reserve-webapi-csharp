#nullable disable
using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class LocationDto
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("coordinates")]
    public long[] Coordinates { get; set; }
}