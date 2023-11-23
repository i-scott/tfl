using System.Threading.Tasks;
using TFLRoadStatus.Application.Core;

namespace TFLRoadStatus.Application
{
    public interface IRoadStatusService
    {
        Task<Result<RoadStatus>> RunAsync(string v);
    }
}