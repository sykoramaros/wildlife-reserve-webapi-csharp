using Microsoft.AspNetCore.Mvc;
using WildlifeReserve.ExternalApis.TestINaturalist;
using WildlifeReserve.ExternalApis.TestINaturalist.QuickType;

namespace WildlifeReserve.ExternalApis.iNaturalist;

[Route("api/[controller]")]
[ApiController]
public class JsonController : ControllerBase {
    [HttpGet("fetch")]
    public async Task<IActionResult> FetchJson(string apiUrl) {

        apiUrl = "https://api.inaturalist.org/v1/observations?place_guess=Prague";
        var httpClient = new HttpClient();
        string jsonData;
        try {
            jsonData = await httpClient.GetStringAsync(apiUrl);
        } catch (HttpRequestException exception) {
            return StatusCode(500, "Error fetching data: " + exception.Message);
        }

        Welcome welcomeObject = Welcome.FromJson(jsonData);
        try {
            welcomeObject = Welcome.FromJson(jsonData);
        }
        catch (Exception exception)
        {
            return StatusCode(500, "Error fetching data: " + exception.Message);
        }
        return Ok(welcomeObject);
    }
}