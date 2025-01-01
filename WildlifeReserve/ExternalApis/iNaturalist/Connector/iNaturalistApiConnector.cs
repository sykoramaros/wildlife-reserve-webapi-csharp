using WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

namespace WildlifeReserve.ExternalApis.iNaturalist.Connector
{
    // Třída iNaturalistApiConnector slouží pro komunikaci s API iNaturalist
    public class iNaturalistApiConnector {
        private readonly HttpClient httpClient;
        private string baseUrl; // Ukládá základní URL pro API požadavky
        
        // Konstruktor
        public iNaturalistApiConnector(HttpClient httpClient, string baseUrl = "https://api.inaturalist.org/v1/observations?place_guess=Prague") {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.baseUrl = baseUrl;
        }

        public string GetBaseUrl() {
            return baseUrl; // Vrací uloženou hodnotu baseUrl
        }

        // FetchJson metoda pro načítání JSON dat z API
        public async Task<string> FetchJson() {
            var response = await httpClient.GetStringAsync("https://api.inaturalist.org/v1/observations?place_guess=Prague");
            return response;
        }

        // Asynchronní metoda pro získání seznamu pozorování
        public async Task<ObservationListDto> FetchObservationListAsync() {
            // Nacte JSON data z API
            string jsonData = await FetchJson();
            // Vrací získaný JSON jako odpověď
            return ObservationListDeserializer.FromJson(jsonData);

        }
    }
}