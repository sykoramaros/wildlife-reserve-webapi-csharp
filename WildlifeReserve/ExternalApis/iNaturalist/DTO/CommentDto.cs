#nullable disable
using Newtonsoft.Json;
using WildlifeReserve.ExternalApis.TestINaturalist.QuickType;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class CommentDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonProperty("created_at_details")]
    public Details CreatedAtDetails { get; set; }

    [JsonProperty("user")]
    public User User { get; set; }

    [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
    public string Body { get; set; }
}