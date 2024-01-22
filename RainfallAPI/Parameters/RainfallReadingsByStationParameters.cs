using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RainfallAPI.Parameters
{
    public class RainfallReadingsByStationParameters
    {
        [FromRoute]
        public string StationId { get; set; }

        [FromQuery]
        [Range(1, 100, ErrorMessage = "Count must be between 1 and 100.")]
        [SwaggerParameter("Count of readings (between 1 and 100).", Required = false)]
        public int Count { get; set; } = 10;
    }
}
