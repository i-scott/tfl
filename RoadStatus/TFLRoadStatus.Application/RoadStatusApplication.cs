using Microsoft.Extensions.Logging;
using TFLRoadStatus.Domain;
using TFLRToadStatus.Interfaces;

namespace TFLRoadStatus.Application
{
    public class RoadStatusApplication : IRoadStatus
    {
        private readonly IRoadStatusService _roadStatusService;
        private readonly ILogger<RoadStatusApplication> _logger;
        private readonly IResultWriter<RoadStatusResult> _roadStatusConsoleWriter;

        public RoadStatusApplication(IRoadStatusService roadStatusService, IResultWriter<RoadStatusResult> roadStatusConsoleWriter, ILogger<RoadStatusApplication> logger)
        {
            _roadStatusService = roadStatusService;
            _roadStatusConsoleWriter = roadStatusConsoleWriter;
            _logger = logger;
        }

        public async Task<Result<RoadStatusResult>> RunAsync(string roadId)
        {
            _logger.LogTrace($"Running {nameof(RoadStatusApplication)}:{nameof(RunAsync)} with roadId {roadId}");

            var result = await _roadStatusService.ExecuteAsync(roadId);

            _roadStatusConsoleWriter.WriteToOutput(result);

            return result;
        }
    }
}