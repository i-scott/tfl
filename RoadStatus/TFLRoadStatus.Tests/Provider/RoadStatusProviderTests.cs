using Moq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using Newtonsoft.Json;
using FluentAssertions;

using TFLRoadStatus.Service;
using TFLRoadStatus.Domain;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace TFLRoadStatus.Tests.Provider
{
    public class RoadStatusProviderTests
    {
        [Fact]
        public async void WhenProviderGets200_ReturnsRoadStatusType()
        {
            var expected = TFLApiResultTypes.GoodStatus();
            var clientHandlerStub = GetDelegatingHandlerStubForRequest(HttpStatusCode.OK, expected);
            var client = new HttpClient(clientHandlerStub);
            var mockLogger = new Mock<ILogger<TFLRoadStatusService>>();

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var sut = new TFLRoadStatusService(mockHttpClientFactory.Object, 
                                            new TFLAppKeySecuredUriProvider("http://localhost", "appId", "appKey"), 
                                            new ValidRoadResponseToRoadStatusMapper(),
                                            mockLogger.Object);

            var result = await sut.ExecuteAsync("A2");

            result.IsSuccess.Should().BeTrue();
            result.Should().BeOfType<Result<RoadStatusResult>>();
        }

        [Fact]
        public async void WhenProviderGets404_ReturnsRoadStatusInError()
        {
            var expected = TFLApiResultTypes.BadStatusNotFound();
            var clientHandlerStub = GetDelegatingHandlerStubForRequest(HttpStatusCode.NotFound, expected);
            var client = new HttpClient(clientHandlerStub);
            var mockLogger = new Mock<ILogger<TFLRoadStatusService>>();

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var sut = new TFLRoadStatusService(mockHttpClientFactory.Object, 
                                            new TFLAppKeySecuredUriProvider("http://localhost", "appId", "appKey"),
                                            new ValidRoadResponseToRoadStatusMapper(),
                                            mockLogger.Object);

            var result = await sut.ExecuteAsync("A2");

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async void WhenProviderGets200_ReturnsRoadStatusWithExpectedValues()
        {
            var expectedSeverity = "This is a test";
            var expectedSeverityDescription = "This is a test";
            var expectedDisplayName = "This is a test";

            var expectedBody = TFLApiResultTypes.GoodStatus(expectedSeverity, expectedSeverityDescription, expectedDisplayName);
            var clientHandlerStub = GetDelegatingHandlerStubForRequest(HttpStatusCode.OK, expectedBody);
            var client = new HttpClient(clientHandlerStub);
            var mockLogger = new Mock<ILogger<TFLRoadStatusService>>();

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var sut = new TFLRoadStatusService(mockHttpClientFactory.Object,
                                            new TFLAppKeySecuredUriProvider("http://localhost", "appId", "appKey"),
                                            new ValidRoadResponseToRoadStatusMapper(),
                                            mockLogger.Object);

            var result = await sut.ExecuteAsync("A2");

            result.IsSuccess.Should().BeTrue();
            result.Value.Severity.Should().Be(expectedSeverity);
            result.Value.SeverityDescription.Should().Be(expectedSeverityDescription);
            result.Value.DisplayName.Should().Be(expectedDisplayName);
        }

        [Fact]
        public async void WhenProviderExecutes_UsesConfiguredURLInformation()
        {
            var urlSetByDelegatingHandlerStub = string.Empty;

            var expectedUrl = "http://localhost/road/A2?app_id=appId&app_key=appKey";

            var data = TFLApiResultTypes.GoodStatus();
            var clientHandlerStub = GetDelegatingHandlerStubForRequest(HttpStatusCode.OK, data);
            var client = new HttpClient(clientHandlerStub);
            var mockLogger = new Mock<ILogger<TFLRoadStatusService>>();

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

            var sut = new TFLRoadStatusService(mockHttpClientFactory.Object,
                                            new TFLAppKeySecuredUriProvider("http://localhost", "appId", "appKey"),
                                            new ValidRoadResponseToRoadStatusMapper(),
                                            mockLogger.Object);

            var result = await sut.ExecuteAsync("A2");

            clientHandlerStub.RequestUrlUsed.Should().Be(expectedUrl);            
        }

        private DelegatingHandlerStub GetDelegatingHandlerStubForRequest(HttpStatusCode statusCode, string content)
        {
            return  new DelegatingHandlerStub((request, cancellationToken) =>
            {
                var response = new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(content)
                };
                return Task.FromResult(response);
            });
        }
    }

    internal static class TFLApiResultTypes
    {
        public static string GoodStatus(string severity = "Good", string severityDescription = "No Exceptional Delays", string displayName = "A2")
        {                        
            return JsonConvert.SerializeObject( new List<ValidRoadResponse> () {
                        new ValidRoadResponse
                        {
                            Type = "Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities",
                            ID = "a2",
                            DisplayName = displayName,
                            StatusSeverity = severity,
                            StatusSeverityDescription = severityDescription,
                            Bounds = "[[-0.0857,51.44091],[0.17118,51.49438]]",
                            Envelope = "[[-0.0857,51.44091],[-0.0857,51.49438],[0.17118,51.49438],[0.17118,51.44091],[-0.0857,51.44091]]",
                            url = "/Road/a2"
                        }
            });
        }

        public static string BadStatusNotFound()
        {
            return JsonConvert.SerializeObject(new InvalidRoadResponse
            {
                Type = "Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities",
                UTCTimeStamp = DateTime.Parse("2017-11-21T14:37:39.7206118Z"),
                ExceptionType = "EntityNotFoundException",
                HttpStatusCode = HttpStatusCode.NotFound,
                HttpStatus = "NotFound",
                RelativeUri = "/Road/A233",
                Message = "The following road id is not recognised: A233"

            });
        }
    }
}
