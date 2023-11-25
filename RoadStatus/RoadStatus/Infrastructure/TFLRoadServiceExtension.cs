using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TFLRoadStatus.Domain;
using TFLRoadStatus.Service;
using TFLRToadStatus.Interfaces;

namespace RoadStatus.Infrastructure
{
    public static class TFLRoadServiceExtension
    {
        public static void AddTFLRoadService( this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient();

            services.AddSingleton<IMapper<ValidRoadResponse, RoadStatusResult>, ValidRoadResponseToRoadStatusMapper>();
            services.AddSingleton<IRoadStatusService, TFLRoadStatusService>();

            services.AddSingleton<IURIProvider>((provider) =>
            {
                var tflSection = config.GetSection("tfl");
                var tflSecuritySection = tflSection.GetSection("secuirty");
                var tflBaseUrl = tflSection.GetValue<string>("baseUrl");

                var tflAppID = tflSecuritySection.GetValue<string>("appId");
                var tflAppKey = tflSecuritySection.GetValue<string>("primaryAppKey");

                if(tflBaseUrl == null || tflAppID == null || tflAppKey == null)
                {
                    throw new Exception("Required configuration is missing");
                }

                return new TFLAppKeySecuredUriProvider(tflBaseUrl, tflAppID, tflAppKey);
            });
        }
    }
}
