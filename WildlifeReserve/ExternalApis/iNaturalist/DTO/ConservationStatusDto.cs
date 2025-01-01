#nullable disable
using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class ConservationStatusDto
{
    [JsonProperty("source_id")]
    public long SourceId { get; set; }

    [JsonProperty("authority")]
    public string Authority { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("status_name")]
    public string StatusName { get; set; }

    [JsonProperty("iucn")]
    public long Iucn { get; set; }

    [JsonProperty("geoprivacy")]
    public string Geoprivacy { get; set; }
}