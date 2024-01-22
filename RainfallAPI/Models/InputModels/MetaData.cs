using Newtonsoft.Json;

namespace RainfallAPI.Models.InputModels
{
    public class MetaData
    {
        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("licence")]
        public string Licence { get; set; }

        [JsonProperty("documentation")]
        public string Documentation { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("hasFormat")]
        public List<string> HasFormat { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }
    }
}
