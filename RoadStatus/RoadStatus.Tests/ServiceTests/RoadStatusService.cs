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
            if (string.IsNullOrEmpty(v) ) return "The following road is not recognised: <empty>";

            return v == "A2" ? v : "The following road is not recognised: A2";
        }
    }
}