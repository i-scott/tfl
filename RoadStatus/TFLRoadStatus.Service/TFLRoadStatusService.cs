using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TFLRoadStatus.Domain;
using TFLRToadStatus.Interfaces;

namespace TFLRoadStatus.Service
{
    public class TFLRoadStatusService : IRoadStatusService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper<List<ValidRoadResponse>, RoadStatusResult> _mapper;
        private readonly IURIProvider _uriProvider;
        private readonly ILogger<TFLRoadStatusService> _logger;

        private const string ROADURLFORMAT = "road/{0}";

        public TFLRoadStatusService(IHttpClientFactory httpClientFactory, IURIProvider uriProvider, IMapper<List<ValidRoadResponse>, RoadStatusResult> mapper, ILogger<TFLRoadStatusService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
            _uriProvider = uriProvider;
            _logger = logger;
        }

        private static string GetRoadURL(string requestedRoad) => string.Format(ROADURLFORMAT, requestedRoad);

        public async Task<Result<RoadStatusResult>> ExecuteAsync(string roadId)
        {
            _logger.LogTrace($"Running {nameof(TFLRoadStatusService)}:{nameof(ExecuteAsync)}", roadId);

            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var roadUrl = GetRoadURL(roadId);

                var uri = _uriProvider.GetUrl(roadUrl);

                var result = await httpClient.GetAsync(uri);

                if (result.IsSuccessStatusCode)
                {
                    var stringData = await result.Content.ReadAsStringAsync();

                    var deserialzedResult = JsonConvert.DeserializeObject<List<ValidRoadResponse>>(stringData);

                    var roadStatus = _mapper.Map(deserialzedResult);

                    return Result<RoadStatusResult>.Success(roadStatus);
                }
                else
                {
                    return Result<RoadStatusResult>.Failure("Unsuccessful Request", new RoadStatusResult { DisplayName = roadId });
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Excpetion thrown in {nameof(TFLRoadStatusService)}:{nameof(ExecuteAsync)}", roadId);
                throw;
            }
        }
    }
}