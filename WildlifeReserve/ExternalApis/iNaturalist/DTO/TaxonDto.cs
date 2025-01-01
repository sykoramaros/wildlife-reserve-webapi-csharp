#nullable disable
using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class TaxonDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("iconic_taxon_id")]
    public long IconicTaxonId { get; set; }

    [JsonProperty("iconic_taxon_name")]
    public string IconicTaxonName { get; set; }

    [JsonProperty("is_active")]
    public bool IsActive { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("preferred_common_name")]
    public string PreferredCommonName { get; set; }

    [JsonProperty("rank")]
    public string Rank { get; set; }

    [JsonProperty("rank_level")]
    public long RankLevel { get; set; }

    [JsonProperty("ancestor_ids")]
    public long[] AncestorIds { get; set; }

    [JsonProperty("ancestry")]
    public string Ancestry { get; set; }

    [JsonProperty("conservation_status")]
    public ConservationStatusDto ConservationStatus { get; set; }

    [JsonProperty("endemic")]
    public bool Endemic { get; set; }

    [JsonProperty("establishment_means")]
    public EstablishmentMeansDto EstablishmentMeans { get; set; }

    [JsonProperty("introduced")]
    public bool Introduced { get; set; }

    [JsonProperty("native")]
    public bool Native { get; set; }

    [JsonProperty("threatened")]
    public bool Threatened { get; set; }
}