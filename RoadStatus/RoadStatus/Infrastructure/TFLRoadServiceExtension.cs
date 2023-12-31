﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Http;
using System;
using System.Collections.Generic;
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
            services.RemoveAll<IHttpMessageHandlerBuilderFilter>();

            services.AddSingleton<IMapper<List<ValidRoadResponse>, RoadStatusResult>, ValidRoadResponseToRoadStatusMapper>();
            services.AddSingleton<IRoadStatusService, TFLRoadStatusService>();

            services.AddSingleton<IURIProvider>((provider) =>
            {
                var tflSection = config.GetSection("tfl");
                var tflSecuritySection = tflSection.GetSection("security");
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
