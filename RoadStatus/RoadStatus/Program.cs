using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

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

            var tflSection = config.GetSection("tfl");
            var tflSecuritySection = tflSection.GetSection("secuirty");
            var tflBaseUrl = tflSection.GetValue<string>("baseUrl");

            var tflAppID = tflSecuritySection.GetValue<string>("appId");
            var tflAppKey = tflSecuritySection.GetValue<string>("primaryAppKey");

            var serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddConsole())                    
                .BuildServiceProvider();



            Console.WriteLine("Hello, World!");
        }
    }
}