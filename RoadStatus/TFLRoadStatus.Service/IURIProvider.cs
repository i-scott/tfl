namespace TFLRoadStatus.Service
{
    public interface IURIProvider
    {
        Uri GetUrl(string requestUrl);
    }
}