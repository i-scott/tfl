using TFLRoadStatus.Domain;

namespace TFLRToadStatus.Interfaces
{
    public interface IResultWriter<T>
    {
        void WriteToOutput(Result<T> result);
    }
}
