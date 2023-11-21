using FluentAssertions;
using Xunit;

namespace RoadStatus.Tests.ServiceTests
{
    public class RoadStatusServiceTests
    {
        [Fact]
        public async void WhenGivenValidRoadID_DisplayNameIsReturned()
        {
            var sut = new RoadStatusService();

            var result = await sut.RunAsync("A2");

            result.Should().Be("A2");
        }

        [Fact]
        public async void WhenGivenInvalidRoadID_RoadNotRecognisedReturned()
        {
            var sut = new RoadStatusService();

            var result = await sut.RunAsync("B3");

            result.Should().Be("The following road is not recognised: A2");
        }

        [Fact]
        public async void WhenGivenNoRoadID_RoadNotRecognisedReturnedWithEmpty() 
        {
            var sut = new RoadStatusService();

            var result = await sut.RunAsync("");

            result.Should().Be("The following road is not recognised: <empty>");
        }

    }
}
