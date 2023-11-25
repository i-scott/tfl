using System;
using TFLRoadStatus.Domain;
using TFLRToadStatus.Interfaces;

namespace RoadStatus.Infrastructure
{
    public class RoadStatusResultConsoleWriter : IResultWriter<RoadStatusResult>
    {
        public void WriteToOutput(Result<RoadStatusResult> result)
        {
            if (result.IsSuccess)
            {
                Console.WriteLine($"The status of the {result.Value.DisplayName} is as follows");
                Console.WriteLine($"        Road Status is {result.Value.Severity}");
                Console.WriteLine($"        Road Status Description is {result.Value.SeverityDescription}");
            }
            else
            {
                Console.WriteLine($"{result.Value.DisplayName} is not a valid road");
            }
        }
    }
}
