namespace RainfallAPI.Models.ViewModels
{
    /// <summary>
    /// Represents an error response.
    /// </summary>
    /// <remarks>Details of an error.</remarks>
    public class ErrorResponse
    {
        public string Message { get; set; }
        public List<ErrorDetail> Detail { get; set; }
    }
}
