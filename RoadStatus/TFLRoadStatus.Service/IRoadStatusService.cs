using TFLRoadStatus.Application;
using TFLRoadStatus.Application.Core;

namespace TFLRoadStatus.Service
{
    public interface IRoadStatusService
    {
        Task<Result<RoadStatus>> Execute(string roadId);
    }
}