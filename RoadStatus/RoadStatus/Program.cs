using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RoadStatus.Infrastructure;
using System;
using System.IO;
using TFLRoadStatus.Application;

namespace RoadStatus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection()
                    .AddLogging(builder => builder.AddConsole());


            serviceCollection.AddTFLRoadService(config);

            serviceCollection.AddSingleton<IRoadStatus, RoadStatusApplication>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            //do the actual work here
            var roadStatus = serviceProvider.GetService<IRoadStatus>();

            roadStatus.RunAsync("A2").GetAwaiter().GetResult();

            Console.WriteLine("Hello, World!");
        }
    }
}