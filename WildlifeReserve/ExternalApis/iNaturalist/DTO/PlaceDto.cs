#nullable disable
using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class PlaceDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("display_name")]
    public string DisplayName { get; set; }
}