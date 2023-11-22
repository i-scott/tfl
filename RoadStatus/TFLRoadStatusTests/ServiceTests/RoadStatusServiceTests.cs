using FluentAssertions;
using TFLRoadStatusApplication;
using Xunit;

namespace TFLRoadStatusTests.ServiceTests
{
    public class RoadStatusServiceTests
    {
        [Fact]
        public async void WhenGivenValidRoadID_DisplayNameIsReturned()
        {
            var sut = new RoadStatusService();

            var result = await sut.RunAsync("A2");

            result.IsSuccess.Should().BeTrue();
            result.Value.DisplayName.Should().Be("A2");
        }

        [Fact]
        public async void WhenGivenValidRoadID_StatusSeverityIsReturned()
        {
            var sut = new RoadStatusService();

            var result = await sut.RunAsync("A2");

            result.IsSuccess.Should().BeTrue();
            result.Value.Severity.Should().Be("Good");
        }

        [Fact]
        public async void WhenGivenValidRoadID_StatusSeverityDescriptionIsReturned()
        {
            var sut = new RoadStatusService();

            var result = await sut.RunAsync("A2");

            result.IsSuccess.Should().BeTrue();
            result.Value.SeverityDescription.Should().Be("No Exceptional Delays");
        }

        [Fact]
        public async void WhenGivenInvalidRoadID_RoadNotRecognisedReturned()
        {
            var sut = new RoadStatusService();

            var result = await sut.RunAsync("B3");

            result.IsSuccess!.Should().BeFalse();
            result.Error.Should().Be("The following road is not recognised: B3");
        }

        [Fact]
        public async void WhenGivenNoRoadID_RoadNotRecognisedReturnedWithEmpty()
        {
            var sut = new RoadStatusService();

            var result = await sut.RunAsync("");

            result.IsSuccess!.Should().BeFalse();
            result.Error.Should().Be("The following road is not recognised: <empty>");
        }
    }
}
