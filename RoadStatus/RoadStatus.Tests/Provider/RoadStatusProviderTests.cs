using FluentAssertions;
using RoadStatusApplication;
using RoadStatusApplication.Core;
using Xunit;


namespace RoadStatusTests.Provider
{
    public class RoadStatusProviderTests
    {
        [Fact]
        public async void WhenProviderGets200_ReturnsRoadStatus()
        {
            var sut = new RoadStatusProvider();

            var result = sut.Execute("A2");

            result.IsSuccess.Should().BeTrue();
            result.Should().BeOfType<Result<RoadStatus>>();
        }

        [Fact]
        public async void WhenProviderGets404_ReturnsRoadStatusInError()
        {
            var sut = new RoadStatusProvider();

            var result = sut.Execute("A2");

            result.IsSuccess.Should().BeFalse();
        }
    }
}
