using WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

namespace WildlifeReserve.ExternalApis.iNaturalist.Connector
{
    // Třída iNaturalistApiConnector slouží pro komunikaci s API iNaturalist
    public class iNaturalistApiConnector {
        private readonly HttpClient httpClient;
        private string baseUrl; // Ukládá základní URL pro API požadavky
        
        // Konstruktor
        public iNaturalistApiConnector(HttpClient httpClient, string baseUrl = "https://api.inaturalist.org/v1/observations") {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.baseUrl = baseUrl;
        }

        public string GetBaseUrl() {
            return baseUrl; // Vrací uloženou hodnotu baseUrl
        }

        // FetchJson metoda pro načítání JSON dat z API
        // Metoda FetchJson umožňuje přidat dotazovací parametry (queryUrl) k základní URL (baseUrl). Pokud žádné parametry nejsou předány, použije pouze baseUrl.
        public async Task<string> FetchJson(string queryUrl = "") {
            string fullUrl = string.IsNullOrWhiteSpace(queryUrl) ? baseUrl : baseUrl + "?" + queryUrl;
            var response = await httpClient.GetStringAsync(fullUrl);
            return response;
        }

        // Asynchronní metoda pro získání seznamu pozorování
        public async Task<ObservationListDto> FetchObservationListAsync(string queryUrl = "") {
            // Nacte JSON data z API
            string jsonData = await FetchJson(queryUrl);
            // Vrací získaný JSON jako odpověď
            return ObservationListDeserializer.FromJson(jsonData);

        }
    }
}