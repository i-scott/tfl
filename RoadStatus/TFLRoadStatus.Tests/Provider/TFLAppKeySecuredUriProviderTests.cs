using FluentAssertions;
using TFLRoadStatus.Service;
using Xunit;

namespace TFLRoadStatus.Tests.Provider
{
    public class TFLAppKeySecuredUriProviderTests
    {
        [Fact]
        public void GetUrlCreatesValidUri()
        {
            var uriProvider = new TFLAppKeySecuredUriProvider("http://localhost", "appId", "appKey");

            var uri = uriProvider.GetUrl("PARAMTOADD");

            uri.OriginalString.Should().Be("http://localhost/PARAMTOADD?app_id=appId&app_key=appKey");
        }
    }
}
