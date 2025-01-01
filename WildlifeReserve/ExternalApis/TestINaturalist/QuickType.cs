// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var welcome = Welcome.FromJson(jsonString);

namespace WildlifeReserve.ExternalApis.TestINaturalist.QuickType
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Welcome
    {
        [JsonProperty("total_results")]
        public long TotalResults { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("per_page")]
        public long PerPage { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
    }

    public partial class Result
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("cached_votes_total")]
        public long CachedVotesTotal { get; set; }

        [JsonProperty("captive")]
        public bool Captive { get; set; }

        [JsonProperty("comments")]
        public Comment[] Comments { get; set; }

        [JsonProperty("comments_count")]
        public long CommentsCount { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset? CreatedAt { get; set; }

        [JsonProperty("created_at_details")]
        public Details CreatedAtDetails { get; set; }

        [JsonProperty("created_time_zone")]
        public string CreatedTimeZone { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("faves_count")]
        public long FavesCount { get; set; }

        [JsonProperty("geojson")]
        public Geojson Geojson { get; set; }

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
        public Comment[] NonOwnerIds { get; set; }

        [JsonProperty("num_identification_agreements")]
        public long NumIdentificationAgreements { get; set; }

        [JsonProperty("num_identification_disagreements")]
        public long NumIdentificationDisagreements { get; set; }

        [JsonProperty("obscured")]
        public bool Obscured { get; set; }

        [JsonProperty("observed_on")]
        public DateTimeOffset? ObservedOn { get; set; }

        [JsonProperty("observed_on_details")]
        public Details ObservedOnDetails { get; set; }

        [JsonProperty("observed_on_string")]
        public string ObservedOnString { get; set; }

        [JsonProperty("observed_time_zone")]
        public string ObservedTimeZone { get; set; }

        [JsonProperty("ofvs")]
        public Ofv[] Ofvs { get; set; }

        [JsonProperty("out_of_range")]
        public bool OutOfRange { get; set; }

        [JsonProperty("photos")]
        public Photo[] Photos { get; set; }

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
        public long SiteId { get; set; }

        [JsonProperty("sounds")]
        public Photo[] Sounds { get; set; }

        [JsonProperty("species_guess")]
        public string SpeciesGuess { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("taxon")]
        public Taxon Taxon { get; set; }

        [JsonProperty("time_observed_at")]
        public DateTimeOffset? TimeObservedAt { get; set; }

        [JsonProperty("time_zone_offset")]
        public string TimeZoneOffset { get; set; }

        [JsonProperty("updated_at")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("verifiable")]
        public bool Verifiable { get; set; }
    }

    public partial class Comment
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

    public partial class Details
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

    public partial class User
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

    public partial class Geojson
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public long[] Coordinates { get; set; }
    }

    public partial class Ofv
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public partial class Photo
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

    public partial class Taxon
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
        public ConservationStatus ConservationStatus { get; set; }

        [JsonProperty("endemic")]
        public bool Endemic { get; set; }

        [JsonProperty("establishment_means")]
        public EstablishmentMeans EstablishmentMeans { get; set; }

        [JsonProperty("introduced")]
        public bool Introduced { get; set; }

        [JsonProperty("native")]
        public bool Native { get; set; }

        [JsonProperty("threatened")]
        public bool Threatened { get; set; }
    }

    public partial class ConservationStatus
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

    public partial class EstablishmentMeans
    {
        [JsonProperty("establishment_means")]
        public string EstablishmentMeansEstablishmentMeans { get; set; }

        [JsonProperty("place")]
        public Place Place { get; set; }
    }

    public partial class Place
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
    }
    // Konvertování JSON do objektů
    public partial class Welcome
    {
        public static Welcome FromJson(string json) => JsonConvert.DeserializeObject<Welcome>(json, WildlifeReserve.ExternalApis.TestINaturalist.QuickType.Converter.Settings);
    }
    // Konvertování objektů do JSON
    public static class Serialize
    {
        public static string ToJson(this Welcome self) => JsonConvert.SerializeObject(self, WildlifeReserve.ExternalApis.TestINaturalist.QuickType.Converter.Settings);
    }
    // Konfigurace pro konvertování JSON
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
