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
}