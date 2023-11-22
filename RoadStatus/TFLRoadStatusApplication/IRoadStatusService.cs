using System.Threading.Tasks;
using TFLRoadStatusApplication.Core;

namespace TFLRoadStatusApplication
{
    public interface IRoadStatusService
    {
        Task<Result<RoadStatus>> RunAsync(string v);
    }
}