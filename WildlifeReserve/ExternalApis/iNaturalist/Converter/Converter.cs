using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

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