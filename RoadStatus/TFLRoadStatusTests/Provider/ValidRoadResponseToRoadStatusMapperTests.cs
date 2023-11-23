
using FluentAssertions;
using TFLRoadStatusProvider;
using Xunit;

namespace TFLRoadStatusTests.Provider
{
    public class ValidRoadResponseToRoadStatusMapperTests
    {
        [Fact]
        public void WhenGivenValidRoadResponseWithSeverity_RoadStatusHasSameSeverity()
        {
            var mapper = new ValidRoadResponseToRoadStatusMapper();

            var sourceValidRoadResponse = new ValidRoadResponse { StatusSeverity = "StatusSeverity" };

            var destinationRoadStatus = mapper.Map(sourceValidRoadResponse);


            destinationRoadStatus.Severity.Should().Be("StatusSeverity");
            destinationRoadStatus.Severity.Should().Be(sourceValidRoadResponse.StatusSeverity);
        }
    }
}
