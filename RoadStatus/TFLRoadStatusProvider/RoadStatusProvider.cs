using TFLRoadStatusApplication;
using TFLRoadStatusApplication.Core;

namespace TFLRoadStatusProvider
{
    public class RoadStatusProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private string baseRequestUri;

        public RoadStatusProvider(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            baseRequestUri = "https://api.tfl.gov.uk/Road";
        }

        private string GetFullURI(string requestedRoad)
        {
            return $"{baseRequestUri}/{requestedRoad}";

        }

        public async Task<Result<RoadStatus>> Execute(string v)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var uri = GetFullURI(v);

            var result = await httpClient.GetAsync(uri);

            if (result.IsSuccessStatusCode)
            {
                return Result<RoadStatus>.Success(new RoadStatus());
            }
            else
            {
                return Result<RoadStatus>.Failure("Unsuccessful Request");
            }
        }
    }
}