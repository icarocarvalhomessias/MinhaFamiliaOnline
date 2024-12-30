using FML.Core.Data;
using FML.Familiares.API.Configuration;
using FML.WebApp.MVC.Services;
using FML.WebApp.MVC.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using System;

namespace Familia.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.RegisterJson();

            services.AddHttpClient<IFamiliaService, FamiliaService>(options =>
            {
                var familiaUrl = configuration.GetSection("FamiliaUrl").Value;
                options.BaseAddress = new Uri(familiaUrl);
            })
                .AddHttpMessageHandler<AuthorizationHandler>()
                .ConfigurePrimaryHttpMessageHandler(() => new CustomHttpClientHandler())
                .AddJsonOptions();

            #region REFIT CONFIG

            var refitSettings = RefitConfig.GetRefitSettings();
            var retryConfig = RefitConfig.GetRetryPolicy();

            services.AddHttpClient("EventoRefit", options =>
            {
                var eventoUrl = configuration.GetSection("EventoUrl").Value;
                options.BaseAddress = new Uri(eventoUrl);
            })
                .AddHttpMessageHandler<AuthorizationHandler>()
                .ConfigurePrimaryHttpMessageHandler(() => new CustomHttpClientHandler())
                .AddTypedClient(client => RestService.For<IEventoServiceRefit>(client, refitSettings))
                .AddPolicyHandler(retryConfig)
                .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            #endregion

            services.AddTransient<AuthorizationHandler>();
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>(options =>
            {
                var autenticacaoUrl = configuration.GetSection("AutenticacaoUrl").Value;
                options.BaseAddress = new Uri(autenticacaoUrl);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new CustomHttpClientHandler())
            .AddJsonOptions();
        }
    }
}
