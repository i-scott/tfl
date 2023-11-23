
using FluentAssertions;
using TFLRoadStatus.Service;
using Xunit;

namespace TFLRoadStatus.Tests.Provider
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

        [Fact]
        public void WhenGivenValidRoadResponseWithSeverityDescriptiion_RoadStatusHasSameSeverity()
        {
            var mapper = new ValidRoadResponseToRoadStatusMapper();

            var sourceValidRoadResponse = new ValidRoadResponse { StatusSeverityDescription = "StatusSeverityDescription" };

            var destinationRoadStatus = mapper.Map(sourceValidRoadResponse);

            destinationRoadStatus.SeverityDescription.Should().Be("StatusSeverityDescription");
            destinationRoadStatus.SeverityDescription.Should().Be(sourceValidRoadResponse.StatusSeverityDescription);
        }

        [Fact]
        public void WhenGivenValidRoadResponseWithDisplayName_RoadStatusHasSameDisplayName()
        {
            var mapper = new ValidRoadResponseToRoadStatusMapper();

            var sourceValidRoadResponse = new ValidRoadResponse { DisplayName = "DisplayName" };

            var destinationRoadStatus = mapper.Map(sourceValidRoadResponse);

            destinationRoadStatus.DisplayName.Should().Be("DisplayName");
            destinationRoadStatus.DisplayName.Should().Be(sourceValidRoadResponse.DisplayName);
        }

    }
}
