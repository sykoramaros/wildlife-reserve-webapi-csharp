using Newtonsoft.Json;

namespace WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

public partial class ObservationListDeserializer {
    public static ObservationListDto FromJson(string json) => JsonConvert.DeserializeObject<ObservationListDto>(json,Converter.Settings);
}