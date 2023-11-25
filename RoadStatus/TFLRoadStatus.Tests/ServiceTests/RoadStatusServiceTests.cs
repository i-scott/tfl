using FluentAssertions;
using Moq;
using TFLRoadStatus.Application;
using TFLRoadStatus.Domain;
using TFLRToadStatus.Interfaces;
using Xunit;

namespace TFLRoadStatus.Tests.ServiceTests
{
    public class RoadStatusServiceTests
    {
        [Fact]
        public async void WhenGivenValidRoadID_DisplayNameIsReturned()
        {
            var mockRoadStatusService = new Mock<IRoadStatusService>();

            mockRoadStatusService.Setup(rs => rs.ExecuteAsync(It.IsAny<string>())).ReturnsAsync( (string roadId) =>
            {
                return Result<RoadStatusResult>.Success(new RoadStatusResult { DisplayName = roadId});
            });

            var sut = new RoadStatusApplication(mockRoadStatusService.Object);

            var result = await sut.RunAsync("A2");

            result.IsSuccess.Should().BeTrue();
            result.Value.DisplayName.Should().Be("A2");
        }

        [Fact]
        public async void WhenGivenValidRoadID_StatusSeverityIsReturned()
        {
            var mockRoadStatusService = new Mock<IRoadStatusService>();

            mockRoadStatusService.Setup(rs => rs.ExecuteAsync(It.IsAny<string>()))
                                    .ReturnsAsync(Result<RoadStatusResult>.Success(new RoadStatusResult { Severity = "Good"}));

            var sut = new RoadStatusApplication(mockRoadStatusService.Object);

            var result = await sut.RunAsync("A2");

            result.IsSuccess.Should().BeTrue();
            result.Value.Severity.Should().Be("Good");
        }

        [Fact]
        public async void WhenGivenValidRoadID_StatusSeverityDescriptionIsReturned()
        {
            var mockRoadStatusService = new Mock<IRoadStatusService>();

            mockRoadStatusService.Setup(rs => rs.ExecuteAsync(It.IsAny<string>()))
                                    .ReturnsAsync(Result<RoadStatusResult>.Success(new RoadStatusResult { SeverityDescription = "No Exceptional Delays" }));

            var sut = new RoadStatusApplication(mockRoadStatusService.Object);

            var result = await sut.RunAsync("A2");

            result.IsSuccess.Should().BeTrue();
            result.Value.SeverityDescription.Should().Be("No Exceptional Delays");
        }

        [Fact]
        public async void WhenGivenInvalidRoadID_RoadNotRecognisedReturned()
        {
            var mockRoadStatusService = new Mock<IRoadStatusService>();

            mockRoadStatusService.Setup(rs => rs.ExecuteAsync(It.IsAny<string>())).ReturnsAsync((string roadId) =>
            {
                return Result<RoadStatusResult>.Failure($"The following road is not recognised: {roadId}");
            });

            var sut = new RoadStatusApplication(mockRoadStatusService.Object);

            var result = await sut.RunAsync("B3");

            result.IsSuccess!.Should().BeFalse();
            result.Error.Should().Be("The following road is not recognised: B3");
        }

        [Fact]
        public async void WhenGivenNoRoadID_RoadNotRecognisedReturnedWithEmpty()
        {
            var mockRoadStatusService = new Mock<IRoadStatusService>();

            mockRoadStatusService.Setup(rs => rs.ExecuteAsync(It.IsAny<string>()))
                                    .ReturnsAsync(Result<RoadStatusResult>.Failure("The following road is not recognised: <empty>"));
            
            var sut = new RoadStatusApplication(mockRoadStatusService.Object);

            var result = await sut.RunAsync("");

            result.IsSuccess!.Should().BeFalse();
            result.Error.Should().Be("The following road is not recognised: <empty>");
        }
    }
}
