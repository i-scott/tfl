using TFLRoadStatus.Domain;

namespace TFLRoadStatus.Application
{
    public interface IRoadStatus
    {
        Task<Result<RoadStatusResult>> RunAsync(string v);
    }
}