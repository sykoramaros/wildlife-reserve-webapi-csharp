// using System.Text.Json;
// using WildlifeReserve.ExternalApis.iNaturalist.DTO;
//
// namespace WildlifeReserve.ExternalApis.iNaturalist.Services;
//
// public class ObservationService {
//     private readonly HttpClient httpClient;
//     private string baseUrl = "https://api.inaturalist.org/v1/observations";
//
//     public ObservationService(HttpClient httpClient) {
//         this.httpClient = httpClient;
//     }
//     // Metoda pro získání pozorování podle TaxonName (podle druhu Animal / Plantae / Fungi)
//     public async Task<List<ObservationDto>>GetObservationsByTaxonName(string taxonName) {
//         try {
//             string url = $"{baseUrl}?taxon_name={taxonName}";
//             var response = await httpClient.GetAsync(url);
//             response.EnsureSuccessStatusCode();
//
//             string responseBody = await response.Content.ReadAsStringAsync();
//             var apiResponse = JsonSerializer.Deserialize<List<ObservationDto>>(responseBody);
//             return apiResponse;
//         } catch (Exception exception) {
//             Console.WriteLine("Error fetching observations: " + exception);
//             return new List<ObservationDto>();
//         }
//     }
//     
//     // Metoda pro získání pozorování podle Name nebo CommonName
//     public async Task<List<ObservationDto>> GetObservationsByNameOrCommonName(string name = null, string commonName = null) {
//         try {
//             if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(commonName)) {
//                 Console.WriteLine("Please type name or common name");
//                 return new List<ObservationDto>();
//             }
//             string url = baseUrl + "?";
//             if (!string.IsNullOrEmpty(name)) {
//                 url += $"name={name}";
//             }
//             if (!string.IsNullOrEmpty(commonName)) {
//                 url += $"common_name={commonName}";
//             }
//             url = url.TrimEnd('&');
//             
//             var response = await httpClient.GetAsync(url);
//             response.EnsureSuccessStatusCode();
//
//             string responseBody = await response.Content.ReadAsStringAsync();
//
//             var apiResponse = JsonSerializer.Deserialize<ObservationApiDto>(responseBody);
//             return apiResponse?.Results ?? new List<ObservationDto>();
//         } catch (Exception exception) {
//             Console.WriteLine("Error fetching observations: " + exception);
//             return new List<ObservationDto>();
//         }
//     }
//     
//     // Metoda pro získání pozorování podle data pozorovani
//     public async Task<List<ObservationDto>> GetObservationsByDate(DateTime date) {
//         try {
//             string url = $"{baseUrl}?observed_on={date.ToString("yyyy-MM-dd")}";
//             var response = await httpClient.GetAsync(url);
//             response.EnsureSuccessStatusCode();
//
//             string responseBody = await response.Content.ReadAsStringAsync();
//             var observations = JsonSerializer.Deserialize<List<ObservationDto>>(responseBody);
//             return observations;
//         } catch (Exception exception) {
//             Console.WriteLine("Error fetching observations: " + exception);
//             return new List<ObservationDto>();
//         }
//     }
//     
//     // Metoda pro získání pozorování podle nazvu mista
//     public async Task<List<ObservationDto>> GetObservationsByPlaceName(string placeName) {
//         try {
//             string url = $"{baseUrl}?place_guess={placeName}";
//             var response = await httpClient.GetAsync(url);
//             response.EnsureSuccessStatusCode();
//
//             string responseBody = await response.Content.ReadAsStringAsync();
//             var observations = JsonSerializer.Deserialize<List<ObservationDto>>(responseBody);
//             return observations;
//         } catch (Exception exception) {
//             Console.WriteLine("Error fetching observations: " + exception);
//             return new List<ObservationDto>();
//         }
//     }
//     
//     // Metoda pro získání pozorování podle geografické polohy (latitude, longitude)
//     public async Task<List<ObservationDto>> GetObservationsByLocation(double latitude, double longitude) {
//         try {
//             string url = $"{baseUrl}?latitude={latitude}&longitude={longitude}";
//             var response = await httpClient.GetAsync(url);
//             response.EnsureSuccessStatusCode();
//
//             string responseBody = await response.Content.ReadAsStringAsync();
//             var observations = JsonSerializer.Deserialize<List<ObservationDto>>(responseBody);
//             return observations;
//         } catch (Exception exception) {
//             Console.WriteLine("Error fetching observations: " + exception);
//             return new List<ObservationDto>();
//         }
//     }
//     
//     
//     
// }