using System;
using System.Threading.Tasks;

namespace RoadStatus.Tests.ServiceTests
{
    public class RoadStatusService
    {
        public RoadStatusService()
        {
        }

        public async Task<Result<ValidRoadStatus>> RunAsync(string v)
        {
            if (string.IsNullOrEmpty(v)) return Result<ValidRoadStatus>.Failure("The following road is not recognised: <empty>");
           

            return v == "A2" ? 
                        Result<ValidRoadStatus>.Success(new ValidRoadStatus{  DisplayName = v, StatusSeverity = "Good", StatusSeverityDescription = "No Exceptional Delays"}) :
                        Result<ValidRoadStatus>.Failure($"The following road is not recognised: {v}")
                        ;
        }
    }
}