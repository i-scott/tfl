using TFLRoadStatus.Domain;
using TFLRToadStatus.Interfaces;

namespace TFLRoadStatus.Application
{
    public class RoadStatusApplication : IRoadStatus
    {
        private readonly IRoadStatusService _roadStatusService;
        public RoadStatusApplication(IRoadStatusService roadStatusService)
        {
            _roadStatusService = roadStatusService;
        }

        public async Task<Result<RoadStatusResult>> RunAsync(string roadId)
        {
            var result = await _roadStatusService.ExecuteAsync(roadId);

            return result;
        }
    }
}