using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RoadStatus.Infrastructure;
using System;
using System.IO;
using TFLRoadStatus.Application;
using TFLRoadStatus.Domain;
using TFLRToadStatus.Interfaces;

namespace RoadStatus
{
    internal class Program
    {
        static int Main(string[] args)
        {
            if(args.Length == 0) 
            {
                Console.WriteLine("Please provide Road ID");
                return -1;
            }

            var requestedRoadId = args[0];

            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection()
                    .AddLogging(builder => builder.AddConsole());

            serviceCollection.AddTFLRoadService(config);

            serviceCollection.AddSingleton<IResultWriter<RoadStatusResult>, RoadStatusResultConsoleWriter>();

            serviceCollection.AddSingleton<IRoadStatus, RoadStatusApplication>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            //do the actual work here
            var roadStatus = serviceProvider.GetService<IRoadStatus>();
            if(roadStatus == null )
            {
                Console.WriteLine("System Error");
                return -2;
            }

            var result = roadStatus.RunAsync(requestedRoadId).GetAwaiter().GetResult();

            return result.IsSuccess ? 0 : 1;
        }
    }
}