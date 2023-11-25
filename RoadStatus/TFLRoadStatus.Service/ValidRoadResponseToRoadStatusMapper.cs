using TFLRoadStatus.Domain;

namespace TFLRoadStatus.Service
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }

    public class ValidRoadResponseToRoadStatusMapper : IMapper<ValidRoadResponse, RoadStatusResult>
    {
        public RoadStatusResult Map(ValidRoadResponse source)
        {
            return new RoadStatusResult
            {
                Severity = source.StatusSeverity,
                SeverityDescription = source.StatusSeverityDescription,
                DisplayName = source.DisplayName
            };
        }
    }
}
