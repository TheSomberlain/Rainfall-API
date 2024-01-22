namespace RainfallAPI.Models.ViewModels
{
    /// <summary>
    /// Represents a rainfall reading response.
    /// </summary>
    /// <remarks>Details of a rainfall reading.</remarks>
    public class RainfallReading
    {
        public DateTime DateMeasured { get; set; }
        public decimal AmountMeasured { get; set; }
    }
}
