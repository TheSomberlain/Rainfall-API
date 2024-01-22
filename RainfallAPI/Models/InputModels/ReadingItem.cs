using Newtonsoft.Json;

namespace RainfallAPI.Models.InputModels
{
    public class ReadingItem
    {
        [JsonProperty("@id")]
        public string Id { get; set; }

        [JsonProperty("dateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("measure")]
        public string Measure { get; set; }

        [JsonProperty("value")]
        public decimal Value { get; set; }
    }
}
