using TFLRoadStatus.Domain;

namespace TFLRoadStatus.Service
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }

    public class ValidRoadResponseToRoadStatusMapper : IMapper<List<ValidRoadResponse>, RoadStatusResult>
    {
        public RoadStatusResult Map(List<ValidRoadResponse> source)
        {
            return new RoadStatusResult
            {
                Severity = source[0].StatusSeverity,
                SeverityDescription = source[0].StatusSeverityDescription,
                DisplayName = source[0].DisplayName
            };
        }
    }
}
