using TFLRToadStatus.Interfaces;

namespace TFLRoadStatus.Service
{
    public class TFLAppKeySecuredUriProvider : IURIProvider
    {
        private string _baseUrl;
        private string _applicationId;
        private string _applicationKey;

        public TFLAppKeySecuredUriProvider(string baseUrl, string applicationId, string applicationKey)
        {
            _baseUrl = baseUrl;
            _applicationId = applicationId;
            _applicationKey = applicationKey;
        }

        public Uri GetUrl(string requestUrl)
        {
            var formattedUrl = $"{_baseUrl}/{requestUrl}?app_id={_applicationId}&app_key={_applicationKey}";

            return new Uri(formattedUrl);
        }
    }
}
