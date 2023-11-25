using TFLRoadStatus.Domain;

namespace TFLRToadStatus.Interfaces
{
    public interface IRoadStatusService
    {
        Task<Result<RoadStatusResult>> ExecuteAsync(string roadId);
    }
}