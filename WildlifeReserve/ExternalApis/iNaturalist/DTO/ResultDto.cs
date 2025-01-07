#nullable disable
using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class ResultDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("cached_votes_total")]
    public long CachedVotesTotal { get; set; }

    [JsonProperty("captive")]
    public bool Captive { get; set; }

    [JsonProperty("comments")]
    public CommentDto[] Comments { get; set; }

    [JsonProperty("comments_count")]
    public long CommentsCount { get; set; }

    [JsonProperty("created_at")]
    public DateTimeOffset? CreatedAt { get; set; }

    [JsonProperty("created_at_details")]
    public DateDto CreatedAtDetails { get; set; }

    [JsonProperty("created_time_zone")]
    public string CreatedTimeZone { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("faves_count")]
    public long FavesCount { get; set; }

    [JsonProperty("geojson")]
    public LocationDto Geojson { get; set; }

    [JsonProperty("geoprivacy")]
    public string Geoprivacy { get; set; }

    [JsonProperty("taxon_geoprivacy")]
    public string TaxonGeoprivacy { get; set; }

    [JsonProperty("id_please")]
    public bool IdPlease { get; set; }

    [JsonProperty("identifications_count")]
    public long IdentificationsCount { get; set; }

    [JsonProperty("identifications_most_agree")]
    public bool IdentificationsMostAgree { get; set; }

    [JsonProperty("identifications_most_disagree")]
    public bool IdentificationsMostDisagree { get; set; }

    [JsonProperty("identifications_some_agree")]
    public bool IdentificationsSomeAgree { get; set; }

    [JsonProperty("license_code")]
    public string LicenseCode { get; set; }

    [JsonProperty("location")]
    public string Location { get; set; }

    [JsonProperty("mappable")]
    public bool Mappable { get; set; }

    [JsonProperty("non_owner_ids")]
    public CommentDto[] NonOwnerIds { get; set; }

    [JsonProperty("num_identification_agreements")]
    public long NumIdentificationAgreements { get; set; }

    [JsonProperty("num_identification_disagreements")]
    public long NumIdentificationDisagreements { get; set; }

    [JsonProperty("obscured")]
    public bool Obscured { get; set; }

    [JsonProperty("observed_on")]
    public DateTimeOffset? ObservedOn { get; set; }

    [JsonProperty("observed_on_details")]
    public DateDto ObservedOnDetails { get; set; }

    [JsonProperty("observed_on_string")]
    public string ObservedOnString { get; set; }

    [JsonProperty("observed_time_zone")]
    public string ObservedTimeZone { get; set; }

    [JsonProperty("ofvs")]
    public ObservationFieldValuesDto[] Ofvs { get; set; }

    [JsonProperty("out_of_range")]
    public bool OutOfRange { get; set; }

    [JsonProperty("photos")]
    public PhotoDto[] Photos { get; set; }

    [JsonProperty("place_guess")]
    public string PlaceGuess { get; set; }

    [JsonProperty("place_ids")]
    public long[] PlaceIds { get; set; }

    [JsonProperty("project_ids")]
    public long[] ProjectIds { get; set; }

    [JsonProperty("project_ids_with_curator_id")]
    public long[] ProjectIdsWithCuratorId { get; set; }

    [JsonProperty("project_ids_without_curator_id")]
    public long[] ProjectIdsWithoutCuratorId { get; set; }

    [JsonProperty("quality_grade")]
    public string QualityGrade { get; set; }

    [JsonProperty("reviewed_by")]
    public long[] ReviewedBy { get; set; }

    [JsonProperty("site_id")]
    public long? SiteId { get; set; }

    [JsonProperty("sounds")]
    public PhotoDto[] Sounds { get; set; }

    [JsonProperty("species_guess")]
    public string SpeciesGuess { get; set; }

    [JsonProperty("tags")]
    public string[] Tags { get; set; }

    [JsonProperty("taxon")]
    public TaxonDto Taxon { get; set; }

    [JsonProperty("time_observed_at")]
    public DateTimeOffset? TimeObservedAt { get; set; }

    [JsonProperty("time_zone_offset")]
    public string TimeZoneOffset { get; set; }

    [JsonProperty("updated_at")]
    public DateTimeOffset? UpdatedAt { get; set; }

    [JsonProperty("uri")]
    public string Uri { get; set; }

    [JsonProperty("user")]
    public ApiUserDto ApiUser { get; set; }

    [JsonProperty("verifiable")]
    public bool Verifiable { get; set; }
}