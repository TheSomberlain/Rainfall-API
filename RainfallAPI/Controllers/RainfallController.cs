using Microsoft.AspNetCore.Mvc;
using RainfallAPI.Interfaces;
using RainfallAPI.Parameters;
using RainfallAPI.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
                var readingsData = await _dataService.GetRainfallDataAsync(stationId, count);
                return Ok(readingsData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
