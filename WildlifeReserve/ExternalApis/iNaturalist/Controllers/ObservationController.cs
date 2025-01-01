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

    // https://api.inaturalist.org/v1/observations?taxon_name={taxonName}
    // FUNGUJE DOBRE
    [HttpGet("byTaxonName")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByTaxonName([FromQuery] string taxonName) {
        if (string.IsNullOrWhiteSpace(taxonName)) {
            return BadRequest("Taxon name is required.");
        }

        string queryUrl = $"taxon_name={taxonName}";
        var result = await observationService.GetObservationListAsync(queryUrl);
        return Ok(result);
    }
}