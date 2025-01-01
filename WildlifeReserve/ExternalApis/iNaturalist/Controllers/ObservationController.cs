using Microsoft.AspNetCore.Mvc;
using WildlifeReserve.ExternalApis.iNaturalist.Services;
using WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;

namespace WildlifeReserve.ExternalApis.iNaturalist.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ObservationController : ControllerBase {
    private readonly ObservationService observationService;
    
    public ObservationController(ObservationService observationService) {
        this.observationService = observationService;
    }
    
    [HttpGet("allObservations")]
    public async Task<ActionResult<ObservationListDto>> GetAllObservations() {
        var observations = await observationService.GetObservationListAsync();
        return Ok(observations);
    }

    // https://api.inaturalist.org/v1/observations?place_guess={placeName}
    // vraci ale vsechna pozorovani celkem
    [HttpGet("byPlace")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByPlaceName([FromQuery] string placeName) {
        if (string.IsNullOrWhiteSpace(placeName)) {
            return BadRequest("Place name is required.");
        }
        string queryUrl = $"place_guess={placeName}";
        var result = await observationService.GetObservationListAsync(queryUrl);
        return Ok(result);
    }
    
    
}