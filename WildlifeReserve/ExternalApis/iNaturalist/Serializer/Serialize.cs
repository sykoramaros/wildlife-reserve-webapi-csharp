using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public static class Serialize
{
    public static string ToJson(this ObservationListDto self) => JsonConvert.SerializeObject(self, Converter.Settings);
}