#nullable disable
using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class ApiUserDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("icon_content_type")]
    public string IconContentType { get; set; }

    [JsonProperty("icon_file_name")]
    public string IconFileName { get; set; }

    [JsonProperty("icon")]
    public string Icon { get; set; }

    [JsonProperty("icon_url")]
    public string IconUrl { get; set; }

    [JsonProperty("login")]
    public string Login { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}