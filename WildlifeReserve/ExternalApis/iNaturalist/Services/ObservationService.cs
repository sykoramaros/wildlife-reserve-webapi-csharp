using WildlifeReserve.ExternalApis.iNaturalist.Connector;
using WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

namespace WildlifeReserve.ExternalApis.iNaturalist.Services;

public class ObservationService {
    private readonly iNaturalistApiConnector apiConnector;

    public ObservationService(iNaturalistApiConnector apiConnector) {
        this.apiConnector = apiConnector;
    }

    public async Task<ObservationListDto> GetObservationListAsync(string queryUrl = "") {
        string jsonData = await apiConnector.FetchJson(queryUrl);

        ObservationListDto observationList = ObservationListDeserializer.FromJson(jsonData);

        return observationList;
    }
}
