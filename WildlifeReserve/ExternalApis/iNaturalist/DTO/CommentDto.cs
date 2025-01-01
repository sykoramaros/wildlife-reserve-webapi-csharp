#nullable disable
using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class CommentDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonProperty("created_at_details")]
    public DateDto CreatedAtDetails { get; set; }

    [JsonProperty("user")]
    public ApiUserDto ApiUser { get; set; }

    [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
    public string Body { get; set; }
}