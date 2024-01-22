using RainfallAPI.Models.InputModels;

namespace RainfallAPI.Interfaces
{
    public interface IRainfallDataService
    {
        Task<IEnumerable<ReadingItem>> GetRainfallDataAsync(string stationId, int count);
    }
}
