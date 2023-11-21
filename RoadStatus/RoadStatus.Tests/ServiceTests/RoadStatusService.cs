using System;
using System.Threading.Tasks;

namespace RoadStatus.Tests.ServiceTests
{
    public class RoadStatusService
    {
        public RoadStatusService()
        {
        }

        public async Task<string> RunAsync(string v)
        {
            return v == "A2" ? v : "The following road is not recognised: A2";
        }
    }
}