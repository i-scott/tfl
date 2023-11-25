using System.Threading.Tasks;
using TFLRoadStatus.Domain;

namespace TFLRToadStatus.Interfaces
{
    public interface IRoadStatus
    {
        Task<Result<RoadStatusResult>> RunAsync(string v);
    }
}