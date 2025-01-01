#nullable disable
using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class DateDto
{
    [JsonProperty("date")]
    public DateTimeOffset Date { get; set; }

    [JsonProperty("day")]
    public long Day { get; set; }

    [JsonProperty("hour")]
    public long Hour { get; set; }

    [JsonProperty("month")]
    public long Month { get; set; }

    [JsonProperty("week")]
    public long Week { get; set; }

    [JsonProperty("year")]
    public long Year { get; set; }
}