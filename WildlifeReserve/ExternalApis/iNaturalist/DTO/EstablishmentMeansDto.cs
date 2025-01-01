#nullable disable
using Newtonsoft.Json;
using WildlifeReserve.ExternalApis.TestINaturalist.QuickType;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class EstablishmentMeansDto
{
    [JsonProperty("establishment_means")]
    public string EstablishmentMeansEstablishmentMeans { get; set; }

    [JsonProperty("place")]
    public Place Place { get; set; }
}