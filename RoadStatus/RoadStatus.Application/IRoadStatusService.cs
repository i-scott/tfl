using RoadStatusApplication.Core;
using System.Threading.Tasks;

namespace RoadStatusApplication
{
    public interface IRoadStatusService
    {
        Task<Result<RoadStatus>> RunAsync(string v);
    }
}