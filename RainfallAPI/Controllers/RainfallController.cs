using AutoMapper;
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
        private readonly IMapper _mapper;
        public RainfallController(IRainfallDataService dataService, IMapper mapper) 
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet("id/{stationId}/readings")]
        [Produces("application/json")]
        [SwaggerOperation(OperationId = "get-rainfall", Summary = "Get rainfall readings by station Id", Description = "Retrieve the latest readings for the specified stationId")]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(RainfallReadingResponse), Description = "A list of rainfall readings successfully retrieved")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse), Description = "Invalid request")]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse), Description = "No readings found for the specified stationId")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse), Description = "Internal server error")]
        public async Task<IActionResult> GetRainfallReadingsByStation(
            [FromRoute]
            [SwaggerParameter("The id of the reading station", Required = true)]   
            string stationId,

            [FromQuery]
            [Range(1, 100, ErrorMessage = "Count must be between 1 and 100.")]
            [SwaggerParameter("The number of readings to return", Required = false)] int count = 10)
        {
            try
            {
                var readingsData = (await _dataService.GetRainfallDataAsync(stationId, count)).ToList();
                if (readingsData == null || readingsData.Count == 0) 
                {
                    return NotFound(new ErrorResponse { Message = "No readings found for the specified stationId." });
                }
                var mappedReadigs = _mapper.Map<IEnumerable<RainfallReading>>(readingsData);
                return Ok(mappedReadigs);
            }
            catch (HttpRequestException ex)
            {
                var errorDetails = ErrorHelper.ExtractErrorDetails(ex.Data);
                var error = new ErrorResponse { Message = "Invalid request.", Detail = errorDetails };
                return StatusCode(StatusCodes.Status400BadRequest, error);
            }
            catch (Exception ex)
            {
                var errorDetails = ErrorHelper.ExtractErrorDetails(ex.Data);
                var error = new ErrorResponse { Message = "Internal Server Error.", Detail = errorDetails };
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }
    }
}
