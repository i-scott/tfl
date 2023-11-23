using TFLRoadStatus.Application.Core;

namespace TFLRoadStatus.Application
{
    public class RoadStatusService : IRoadStatusService
    {
        public RoadStatusService()
        {
        }

        public async Task<Result<RoadStatus>> RunAsync(string v)
        {
            if (string.IsNullOrEmpty(v)) return
                Result<RoadStatus>.Failure("The following road is not recognised: <empty>");

            return v == "A2" ?
                        Result<RoadStatus>.Success(new RoadStatus { DisplayName = v, Severity = "Good", SeverityDescription = "No Exceptional Delays" }) :
                        Result<RoadStatus>.Failure($"The following road is not recognised: {v}")
                        ;
        }
    }
}