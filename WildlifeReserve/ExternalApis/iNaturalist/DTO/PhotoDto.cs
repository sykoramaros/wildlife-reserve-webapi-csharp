#nullable disable
using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class PhotoDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("attribution")]
    public string Attribution { get; set; }

    [JsonProperty("license_code")]
    public string LicenseCode { get; set; }

    [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
    public string Url { get; set; }
}