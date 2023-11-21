using Application.Core;
using System.Threading.Tasks;

namespace Application
{
    public interface IRoadStatusService
    {
        Task<Result<RoadStatus>> RunAsync(string v);
    }
}