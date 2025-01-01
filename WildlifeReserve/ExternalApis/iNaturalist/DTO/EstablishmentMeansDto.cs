
using Newtonsoft.Json;
namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class EstablishmentMeansDto
{
    [JsonProperty("establishment_means")]
    public string EstablishmentMeansEstablishmentMeans { get; set; }

    [JsonProperty("place")]
    public PlaceDto Place { get; set; }
}