using Newtonsoft.Json;
using TFLRoadStatus.Application;
using TFLRoadStatus.Application.Core;

namespace TFLRoadStatus.Service
{
    public class TFLRoadStatusService : IRoadStatusService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper<ValidRoadResponse, RoadStatus> _mapper;
        private readonly IURIProvider _uriProvider;

        private const string ROADURLFORMAT = "road/{0}";

        public TFLRoadStatusService(IHttpClientFactory httpClientFactory, IURIProvider uriProvider, IMapper<ValidRoadResponse, RoadStatus> mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _uriProvider = uriProvider;
        }

        private static string GetRoadURL(string requestedRoad) => string.Format(ROADURLFORMAT, requestedRoad);

        public async Task<Result<RoadStatus>> Execute(string roadId)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var roadUrl = GetRoadURL(roadId);

            var uri = _uriProvider.GetUrl(roadUrl);

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