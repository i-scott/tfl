using TFLRoadStatusApplication;

namespace TFLRoadStatusProvider
{
    public interface IMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);

    }

    public class ValidRoadResponseToRoadStatusMapper : IMapper<ValidRoadResponse, RoadStatus>
    {
        public RoadStatus Map(ValidRoadResponse source)
        {
            return new RoadStatus
            {
                Severity = source.StatusSeverity,
                SeverityDescription = source.StatusSeverityDescription,
                DisplayName = source.DisplayName
            };
        }
    }
}
