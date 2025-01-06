using Microsoft.AspNetCore.Mvc;
using WildlifeReserve.ExternalApis.iNaturalist.Services;
using WildlifeReserve.ExternalApis.TestINaturalist.ObservationDto;
using Swashbuckle.AspNetCore.Annotations;
using WildlifeReserve.Enums;
using WildlifeReserve.Services;
using System.Linq;

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
    
    [HttpGet("byAnimalTaxonName")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByAnimalTaxonName([FromQuery] string taxonName) {
        if (string.IsNullOrWhiteSpace(taxonName)) {
            return BadRequest("Animal taxon name is required.");
        }
        string queryUrl = $"taxon_name={taxonName}";
        var result = await observationService.GetObservationListAsync(queryUrl);
        var filteredResults = result.Results
            .Where(o => o.Taxon != null &&
                o.Taxon.IconicTaxonName == "Animalia" || 
                o.Taxon.IconicTaxonName == "Mammalia" || 
                o.Taxon.IconicTaxonName == "Actinopterygii" || 
                o.Taxon.IconicTaxonName == "Mollusca" || 
                o.Taxon.IconicTaxonName == "Aves")
            .ToList();
        if (filteredResults.Count == 0) {
            return NotFound("No observations found.");
        }
        return Ok(filteredResults);
    }
    
    [HttpGet("byPlantTaxonName")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByPlantTaxonName([FromQuery] string taxonName) {
        if (string.IsNullOrWhiteSpace(taxonName)) {
            return BadRequest("Plant taxon name is required.");
        }
        string queryUrl = $"taxon_name={taxonName}";
        var result = await observationService.GetObservationListAsync(queryUrl);
        var filteredResults = result.Results
            .Where(o => o.Taxon != null && o.Taxon.IconicTaxonName == "Plantae").ToList();
        if (filteredResults.Count == 0) {
            return NotFound("No observations found.");
        }
        return Ok(filteredResults);
    }
    
    [HttpGet("byInsectTaxonName")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByInsectTaxonName([FromQuery] string taxonName) {
        if (string.IsNullOrWhiteSpace(taxonName)) {
            return BadRequest("Plant taxon name is required.");
        }
        string queryUrl = $"taxon_name={taxonName}";
        var result = await observationService.GetObservationListAsync(queryUrl);
        var filteredResults = result.Results
            .Where(o => o.Taxon != null && o.Taxon.IconicTaxonName == "Insecta").ToList();
        if (filteredResults.Count == 0) {
            return NotFound("No observations found.");
        }
        return Ok(filteredResults);
    }
    
    [HttpGet("byFungiTaxonName")]
    public async Task<ActionResult<ObservationListDto>> GetObservationsByFungiTaxonName([FromQuery] string taxonName) {
        if (string.IsNullOrWhiteSpace(taxonName)) {
            return BadRequest("Plant taxon name is required.");
        }
        string queryUrl = $"taxon_name={taxonName}";
        var result = await observationService.GetObservationListAsync(queryUrl);
        var filteredResults = result.Results
            .Where(o => o.Taxon != null && o.Taxon.IconicTaxonName == "Fungi").ToList();
        if (filteredResults.Count == 0) {
            return NotFound("No observations found.");
        }
        return Ok(filteredResults);
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
            queryParams.Add($"taxon_name={Uri.EscapeDataString(taxonName.ToLower())}*");
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
    
    // [HttpGet("byMultipleFilters")]
    // public async Task<ActionResult<ObservationListDto>> GetObservationsByMultipleFilters(
    //     [FromQuery] string? taxonName,
    //     [FromQuery] double? nelat,
    //     [FromQuery] double? nelng,
    //     [FromQuery] double? swlat,
    //     [FromQuery] double? swlng,
    //     [FromQuery] double? lat,
    //     [FromQuery] double? lng,
    //     [FromQuery] double? radius,
    //     [FromQuery] int? day,
    //     [FromQuery] int? month,
    //     [FromQuery] int? year,
    //     [FromQuery] bool? identified,
    //     [FromQuery] Place? place,
    //     [FromQuery] string? iconicTaxonName // Přidáme nový parametr pro iconicTaxonName
    // ) {
    //     var queryParams = new List<string>();
    //
    //     if (place.HasValue) {
    //         var placeDetails = PlaceService.GetPlaceDetails(place.Value);
    //         lat = placeDetails.Lat;
    //         lng = placeDetails.Lng;
    //         radius = placeDetails.Radius;
    //     }
    //
    //     if (!string.IsNullOrWhiteSpace(taxonName)) {
    //         queryParams.Add($"taxon_name={Uri.EscapeDataString(taxonName.ToLower())}*");
    //     }
    //
    //     if (nelat.HasValue && nelng.HasValue && swlat.HasValue && swlng.HasValue) {
    //         queryParams.Add($"nelat={nelat}&nelng={nelng}&swlat={swlat}&swlng={swlng}");
    //     }
    //
    //     if (lat.HasValue && lng.HasValue && radius.HasValue) {
    //         if (lat < -90 || lat > 90) {
    //             return BadRequest("Invalid lat value. It must be between -90 and 90.");
    //         }
    //
    //         if (lng < -180 || lng > 180) {
    //             return BadRequest("Invalid lng value. It must be between -180 and 180.");
    //         }
    //
    //         if (radius <= 0) {
    //             return BadRequest("Invalid radius value. It must be greater than 0.");
    //         }
    //
    //         queryParams.Add($"lat={lat}&lng={lng}&radius={radius}");
    //     }
    //
    //     if (year.HasValue) {
    //         if (year < 2008 || year > DateTime.Now.Year) {
    //             return BadRequest("Invalid date. It must be between 2008-01-01 and now.");
    //         }
    //
    //         if (month.HasValue) {
    //             if (month < 1 || month > 12) {
    //                 return BadRequest("Invalid month value. It must be between 1 and 12.");
    //             }
    //
    //             if (day.HasValue) {
    //                 if (day < 1 || day > 31) {
    //                     return BadRequest("Invalid day value. It must be between 1 and 31.");
    //                 }
    //
    //                 queryParams.Add($"day={day}&month={month}&year={year}");
    //             } else {
    //                 queryParams.Add($"month={month}&year={year}");
    //             }
    //         } else {
    //             queryParams.Add($"year={year}");
    //         }
    //     } else if (month.HasValue || day.HasValue) {
    //         return BadRequest("Invalid date. It must be between 2008-01-01 and now.");
    //     }
    //
    //     if (identified.HasValue) {
    //         queryParams.Add($"identified={identified.ToString().ToLower()}");
    //     }
    //
    //     // Přidání filtrů podle iconicTaxonName (dle kategorie: zvíře, rostlina, hmyz nebo houba)
    //     if (!string.IsNullOrWhiteSpace(iconicTaxonName)) {
    //         // Kontrola, zda iconicTaxonName je "Plantae" (rostlina), "Insecta" (hmyz), "Fungi" (houba) nebo nějaké zvíře
    //         if (iconicTaxonName == "Plantae") {
    //             queryParams.Add(
    //                 $"iconic_taxon_name={Uri.EscapeDataString(iconicTaxonName)}"); // Přidáme filtr pro rostliny
    //         } else if (new List<string> { "Animalia", "Mammalia", "Actinopterygii", "Mollusca", "Aves" }.Contains(
    //                        iconicTaxonName)) {
    //             queryParams.Add(
    //                 $"iconic_taxon_name={Uri.EscapeDataString(iconicTaxonName)}"); // Přidáme filtr pro zvířata
    //         } else if (iconicTaxonName == "Insecta") {
    //             queryParams.Add($"iconic_taxon_name={Uri.EscapeDataString(iconicTaxonName)}"); // Přidáme filtr pro hmyz
    //         } else if (iconicTaxonName == "Fungi") {
    //             queryParams.Add(
    //                 $"iconic_taxon_name={Uri.EscapeDataString(iconicTaxonName)}"); // Přidáme filtr pro houby
    //         } else {
    //             return BadRequest(
    //                 "Invalid iconic taxon name. Valid values are 'Plantae' for plants, 'Insecta' for insects, 'Fungi' for fungi, or one of 'Animalia', 'Mammalia', 'Actinopterygii', 'Mollusca', 'Aves' for animals.");
    //         }
    //     }
    //
    //     // kontrola zda byl zadan alespon jeden filtr
    //     if (queryParams.Count == 0) {
    //         return BadRequest("At least one filter is required.");
    //     }
    //
    //     string queryUrl = string.Join("&", queryParams);
    //
    //     try {
    //         var result = await observationService.GetObservationListAsync(queryUrl);
    //
    //         // Pokud byl použit filtr pro iconicTaxonName, aplikujeme filtraci i na výsledky
    //         if (!string.IsNullOrWhiteSpace(iconicTaxonName)) {
    //             // Aplikujeme filtr pro rostliny, hmyz, houby nebo zvířata
    //             if (iconicTaxonName == "Plantae") {
    //                 result.Results = result.Results
    //                     .Where(o => o.Taxon != null && o.Taxon.IconicTaxonName == "Plantae")
    //                     .ToArray();
    //             } else if (new List<string> { "Animalia", "Mammalia", "Actinopterygii", "Mollusca", "Aves" }.Contains(
    //                            iconicTaxonName)) {
    //                 result.Results = result.Results
    //                     .Where(o => o.Taxon != null && new List<string>
    //                             { "Animalia", "Mammalia", "Actinopterygii", "Mollusca", "Aves" }
    //                         .Contains(o.Taxon.IconicTaxonName))
    //                     .ToArray();
    //             } else if (iconicTaxonName == "Insecta") {
    //                 result.Results = result.Results
    //                     .Where(o => o.Taxon != null && o.Taxon.IconicTaxonName == "Insecta")
    //                     .ToArray();
    //             } else if (iconicTaxonName == "Fungi") {
    //                 result.Results = result.Results
    //                     .Where(o => o.Taxon != null && o.Taxon.IconicTaxonName == "Fungi")
    //                     .ToArray();
    //             }
    //         }
    //
    //         return Ok(result);
    //     } catch (Exception exception) {
    //         return StatusCode(500, "An error occurred while processing the request.");
    //     }
    // }
}