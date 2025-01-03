﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace FML.Familiares.API.Configuration
{
    public static class JsonConfig
    {
        public static void RegisterJson(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        options.JsonSerializerOptions.MaxDepth = 64; // Increase the maximum depth if needed
                    });
        }

        public static IHttpClientBuilder AddJsonOptions(this IHttpClientBuilder builder)
        {
            builder.Services.Configure<JsonOptions>(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.MaxDepth = 64; // Increase the maximum depth if needed
            });

            return builder;
        }
    }
}
