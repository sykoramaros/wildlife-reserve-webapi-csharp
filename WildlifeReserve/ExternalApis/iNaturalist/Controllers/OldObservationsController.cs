// using Microsoft.AspNetCore.Mvc;
// using WildlifeReserve.ExternalApis.iNaturalist.Models;
// using WildlifeReserve.ExternalApis.iNaturalist.Services;
//
// namespace WildlifeReserve.ExternalApis.iNaturalist.Controllers;
//
// [Route("api/[controller]")]
// [ApiController]
// public class ObservationsController : ControllerBase {
//     private ObservationService observationService;
//
//     public ObservationsController(ObservationService observationService) {
//         this.observationService = observationService ?? throw new ArgumentNullException(nameof(observationService));
//     }
//     
//     // GET: api/observations?taxonName={taxonName}
//     [HttpGet("byTaxonName")]
//     public async Task<ActionResult<List<ObservationDto>>> GetObservationsByTaxonName([FromQuery] string taxonName) {
//         var observations = await observationService.GetObservationsByTaxonName(taxonName);
//         if (observations == null || !observations.Any()) {
//             return NotFound("From ObservationsController (byTaxonName): No observations found matching the criteria.");
//         }
//         return Ok(observations);
//     }
//     
//     // GET: api/observations?name={name}&commonName={commonName}
//     [HttpGet("byNameOrCommonName")]
//     public async Task<ActionResult<List<ObservationDto>>> GetObservationsByNameOrCommonName([FromQuery] string name = null, [FromQuery] string commonName = null) {
//         if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(commonName)) {
//             return BadRequest("Please type name or common name");
//         }
//         var observations = await observationService.GetObservationsByNameOrCommonName(name, commonName);
//         if (observations == null || !observations.Any()) {
//             
//             return NotFound("From ObservationsController (byNameOrCommonName): No observations found matching the criteria.");
//         }
//         return Ok(observations);
//     }
//     
//     // GET: api/observations?place_guess={placeName}
//     [HttpGet]
//     public async Task<ActionResult<List<ObservationDto>>> GetObservationsByPlaceName([FromQuery] string placeName) {
//         var observations = await observationService.GetObservationsByPlaceName(placeName);
//         if (observations == null || !observations.Any()) {
//             return NotFound("From ObservationsController (byPlaceName): No observations found matching the criteria.");
//         }
//         return Ok(observations);
//     }
// }