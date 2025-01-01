using Microsoft.AspNetCore.Mvc;
using WildlifeReserve.ExternalApis.iNaturalist.Services;
using WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;
using Swashbuckle.AspNetCore.Annotations;
using WildlifeReserve.Enums;
using WildlifeReserve.Services;

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

    // https://api.inaturalist.org/v1/observations?nelat={nelat}&nelng={nelng}&swlat={swlat}&swlng={swlng}
    // FUNGUJE DOBRE
    [HttpGet("byGeoLocation")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByGeoLocation(
        [FromQuery] [SwaggerParameter("Between -90 and 90")]
        double nelat,
        [FromQuery] [SwaggerParameter("Between -180 and 180")]
        double nelng,
        [FromQuery] [SwaggerParameter("Between -90 and 90")]
        double swlat,
        [FromQuery] [SwaggerParameter("Between -180 and 180")]
        double swlng) {
        // osetreni platnosti geografickych souradnic
        if (nelat < -90 || nelat > 90) {
            return BadRequest("Invalid nelat value. It must be between -90 and 90.");
        }
        if (nelng < -180 || nelng > 180) {
            return BadRequest("Invalid nelng value. It must be between -180 and 180.");
        }
        if (swlat < -90 || swlat > 90) {
            return BadRequest("Invalid swlat value. It must be between -90 and 90.");
        }
        if (swlng < -180 || swlng > 180) {
            return BadRequest("Invalid swlng value. It must be between -180 and 180.");
        }
        string queryUrl = $"nelat={nelat}&nelng={nelng}&swlat={swlat}&swlng={swlng}";
        var result = await observationService.GetObservationListAsync(queryUrl);
        return Ok(result);
    }

    [HttpGet("byPoint")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByPoint(
        [FromQuery] [SwaggerParameter("X -90 and 90")]
        double lat,
        [FromQuery] [SwaggerParameter("Y -180 and 180")]
        double lng,
        [FromQuery] [SwaggerParameter("Point size in km")]
        double radius) {
        if (lat < -90 || lat > 90) {
            return BadRequest("Invalid lat value. It must be between -90 and 90.");
        }
        if (lng < -180 || lng > 180) {
            return BadRequest("Invalid lng value. It must be between -180 and 180.");
        }
        if (radius <= 0) {
            return BadRequest("Invalid radius value. It must be greater than 0.");
        }
        string queryUrl = $"lat={lat}&lng={lng}&radius={radius}";
        var result = await observationService.GetObservationListAsync(queryUrl);
        return Ok(result);
    }

    [HttpGet("byDate")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByDate(
        [FromQuery] int day,
        [FromQuery] int month,
        [FromQuery] [SwaggerParameter("Between 2008 and now")] int year) {
        if (day < 1 || day > 31 || month < 1 || month > 12 || year < 2008 || year > DateTime.Now.Year) {
            return BadRequest("Invalid date. It must be between 2008-01-01 and now.");
        }
        string queryUrl = $"day={day}&month={month}&year={year}";
        var result = await observationService.GetObservationListAsync(queryUrl);
        return Ok(result);
    }
    
    [HttpGet("byIdentification")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByIdentification([FromQuery] bool identified = false) {   // defaultne je False a True s velkmym prvnim pismenem a proto je potreba je zmensit
        string queryUrl = $"identified={identified.ToString().ToLower()}";
        var result = await observationService.GetObservationListAsync(queryUrl);
        return Ok(result);  
    }
    
    [HttpGet("byMultipleFilters")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByMultipleFilters(
        [FromQuery] string? taxonName,
        [FromQuery] double? nelat,
        [FromQuery] double? nelng,
        [FromQuery] double? swlat,
        [FromQuery] double? swlng,
        [FromQuery] double? lat,
        [FromQuery] double? lng,
        [FromQuery] double? radius,
        [FromQuery] int? day,
        [FromQuery] int? month,
        [FromQuery] int? year,
        [FromQuery] bool? identified,
        [FromQuery] Place? place) {
        var queryParams = new List<string>();
        
        if (place.HasValue) {
            var placeDetails = PlaceService.GetPlaceDetails(place.Value);
            lat = placeDetails.Lat;
            lng = placeDetails.Lng;
            radius = placeDetails.Radius;
        }
        
        if (!string.IsNullOrWhiteSpace(taxonName)) {
            queryParams.Add($"taxon_name={taxonName}");
        }
        if (nelat.HasValue && nelng.HasValue && swlat.HasValue && swlng.HasValue) {
            queryParams.Add($"nelat={nelat}&nelng={nelng}&swlat={swlat}&swlng={swlng}");
        }
        if (lat.HasValue && lng.HasValue && radius.HasValue) {
            if (lat < -90 || lat > 90) {
                return BadRequest("Invalid lat value. It must be between -90 and 90.");
            }
            if (lng < -180 || lng > 180) {
                return BadRequest("Invalid lng value. It must be between -180 and 180.");
            }
            if (radius <= 0) {
                return BadRequest("Invalid radius value. It must be greater than 0.");
            }
            queryParams.Add($"lat={lat}&lng={lng}&radius={radius}");
        }
        if (year.HasValue) {
            if (year < 2008 || year > DateTime.Now.Year) {
                return BadRequest("Invalid date. It must be between 2008-01-01 and now.");
            }
            if (month.HasValue) {
                if (month < 1 || month > 12) {
                    return BadRequest("Invalid month value. It must be between 1 and 12.");
                }
                if (day.HasValue) {
                    if (day < 1 || day > 31) {
                        return BadRequest("Invalid day value. It must be between 1 and 31.");
                    }
                    queryParams.Add($"day={day}&month={month}&year={year}");
                } else {
                    queryParams.Add($"month={month}&year={year}");
                }
            } else {
                queryParams.Add($"year={year}");
            }
        } else if (month.HasValue || day.HasValue) {
            return BadRequest("Invalid date. It must be between 2008-01-01 and now.");
        }
        if (identified.HasValue) {
            queryParams.Add($"identified={identified.ToString().ToLower()}");
        }
        // kontrola zda byl zadan alespon jeden filtr
        if (queryParams.Count == 0) {
            return BadRequest("At least one filter is required.");
        }
        string queryUrl = string.Join("&", queryParams);

        try {
            var result = await observationService.GetObservationListAsync(queryUrl);
            return Ok(result);
        } catch (Exception exception) {
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}