using Newtonsoft.Json;
using TFLRoadStatusApplication;
using TFLRoadStatusApplication.Core;

namespace TFLRoadStatusProvider
{
    public class RoadStatusProvider
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper<ValidRoadResponse, RoadStatus> _mapper;
        private string baseRequestUri;

        public RoadStatusProvider(IHttpClientFactory httpClientFactory, IMapper<ValidRoadResponse, RoadStatus> mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
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
                var stringData = await result.Content.ReadAsStringAsync();

                var deserialzedResult = JsonConvert.DeserializeObject<ValidRoadResponse>(stringData);

                var roadStatus = _mapper.Map(deserialzedResult);

                return Result<RoadStatus>.Success(roadStatus);
            }
            else
            {
                return Result<RoadStatus>.Failure("Unsuccessful Request");
            }
        }
    }
}