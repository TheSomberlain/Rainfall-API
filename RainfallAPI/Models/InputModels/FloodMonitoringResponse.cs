using Newtonsoft.Json;

namespace RainfallAPI.Models.InputModels
{
    public class FloodMonitoringResponse
    {
        [JsonProperty("@context")]
        public string Context { get; set; }

        [JsonProperty("meta")]
        public MetaData Meta { get; set; }

        [JsonProperty("items")]
        public List<ReadingItem> Items { get; set; }
    }
}
