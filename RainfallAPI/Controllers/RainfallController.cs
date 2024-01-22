using Microsoft.AspNetCore.Mvc;
using RainfallAPI.Interfaces;
using RainfallAPI.Models.ResponseModels;
using RainfallAPI.Models.ViewModels;
using RainfallAPI.Utils;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RainfallAPI.Controllers
{
    [Route("rainfall")]
    [ApiController]
    public class RainfallController : ControllerBase
    {
        private readonly IRainfallDataService _dataService;
        public RainfallController(IRainfallDataService dataService) 
        {
            _dataService = dataService;
        }

        [HttpGet("id/{stationId}/readings")]
        [SwaggerOperation(Summary = "Get Rainfall Readings", Description = "Retrieve rainfall data for a specific station.")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(RainfallReadingResponse), Description = "A list of rainfall readings successfully retrieved")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Error), Description = "Invalid request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(Error), Description = "No readings found for the specified stationId")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(Error), Description = "Internal server error")]
        public async Task<IActionResult> GetRainfallReadingsByStation(
            [FromRoute]
            [SwaggerParameter("The id of the reading station.", Required = true)]   
            string stationId,

            [FromQuery]
            [Range(1, 100, ErrorMessage = "Count must be between 1 and 100.")]
            [SwaggerParameter("Count of readings (between 1 and 100).", Required = false)] int count = 10)
        {
            try
            {
                var readingsData = (await _dataService.GetRainfallDataAsync(stationId, count)).ToList();
                if (readingsData == null || readingsData.Count == 0) 
                {
                    return NotFound(new Error { Message = "No readings found for the specified stationId." });
                }
                return Ok(readingsData);
            }
            catch (HttpRequestException ex)
            {
                var errorDetails = ErrorHelper.ExtractErrorDetails(ex.Data);
                var error = new Error { Message = "Invalid request.", Detail = errorDetails };
                return StatusCode(StatusCodes.Status400BadRequest, error);
            }
            catch (Exception ex)
            {
                var errorDetails = ErrorHelper.ExtractErrorDetails(ex.Data);
                var error = new Error { Message = "Internal Server Error.", Detail = errorDetails };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
    }
}
