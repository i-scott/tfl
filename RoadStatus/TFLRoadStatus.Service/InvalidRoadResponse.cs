using System.Net;
using System.Text.Json;
using Newtonsoft.Json;

namespace TFLRoadStatus.Service
{
    public class InvalidRoadResponse
    {
        [JsonProperty("$type")]
        public string? Type { get; set; }

        [JsonProperty("timestampUtc")]
        public DateTime? UTCTimeStamp { get; set; }

        [JsonProperty("exceptionType")]
        public string? ExceptionType { get; set; }

        [JsonProperty("httpStatusCode")]
        public HttpStatusCode HttpStatusCode { get; set; }

        [JsonProperty("httpStatus")]
        public string? HttpStatus { get; set; }

        [JsonProperty("relativeUri")]
        public string? RelativeUri { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
