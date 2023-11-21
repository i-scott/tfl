using RoadStatusApplication.Core;
using RoadStatusApplication;
using System;

namespace RoadStatusTests.Provider
{
    public class RoadStatusProvider
    {
        public RoadStatusProvider()
        {
        }

        public Result<RoadStatus> Execute(string v)
        {
            return new Result<RoadStatus>();
        }
    }
}