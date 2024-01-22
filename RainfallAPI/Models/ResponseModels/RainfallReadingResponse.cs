using RainfallAPI.Models.ViewModels;

namespace RainfallAPI.Models.ResponseModels
{
    /// <summary>
    /// Represents a rainfall reading response
    /// </summary>
    /// <remarks>Details of a rainfall reading</remarks>
    public class RainfallReadingResponse
    {
        public List<RainfallReading> Readings { get; set; }
    }
}
