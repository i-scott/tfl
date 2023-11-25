using System.Text.Json;
using Newtonsoft.Json;

namespace TFLRoadStatus.Service
{
    public class ValidRoadResponse
    {
        [JsonProperty("$type")]
        public string? Type { get; set; }

        [JsonProperty("id")]
        public string? ID { get; set; }

        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }

        [JsonProperty("statusSeverity")]
        public string? StatusSeverity { get; set; }

        [JsonProperty("statusSeverityDescription")]
        public string? StatusSeverityDescription { get; set; }

        [JsonProperty("bounds")]
        public string? Bounds { get; set; }

        [JsonProperty("envelope")]
        public string? Envelope { get; set; }

        [JsonProperty("url")]
        public string? url { get; set; }
    }
}
