// <auto-generated />
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//    using QuickType;
//    var welcome = Welcome.FromJson(jsonString);

using WildlifeReserve.ExternalApis.iNaturalist.Connector;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public partial class ObservationListDto
    {
        [JsonProperty("total_results")]
        public long TotalResults { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("per_page")]
        public long PerPage { get; set; }

        [JsonProperty("results")]
        public ResultDto[] Results { get; set; }
    }
}