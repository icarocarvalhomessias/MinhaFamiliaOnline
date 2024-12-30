using FML.Familiares.API.Data;
using FML.Familiares.API.Data.Repository.Interface;
using FML.Familiares.API.Data.Repository;
using FML.Familiares.API.Services;
using FML.Familiares.API.Services.Interface;
using FML.Core.Mediator;
using MediatR;
using FML.Familiares.API.Application.Commands;
using FluentValidation.Results;
using FML.Familiares.API.Application.Events;
using FML.Core.Data;
using FML.Familiares.API.Clients;

namespace FML.Familiares.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarFamiliarCommand, ValidationResult>, FamiliarCommandHandler>();
            services.AddScoped<INotificationHandler<FamiliarRegistradoEvent>, FamiliarEventHandler>();

            services.AddScoped<IRelativeRepository, RelativeRepository>();
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IFamilyRepository, FamilyRepository>();


            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IRelativeService, RelativeService>();
            services.AddScoped<IHouseService, HouseService>();

            //set base address here
            services.AddHttpClient<IFileHttp, FileHttp>(client =>
            {
                var fileUrl = configuration["FileUrl"];
                if (string.IsNullOrEmpty(fileUrl))
                {
                    throw new ArgumentNullException(nameof(fileUrl), "FileUrl environment variable is not set.");
                }
                client.BaseAddress = new Uri(fileUrl);
            })
            .AddHttpMessageHandler<AuthorizationHandler>()
            .ConfigurePrimaryHttpMessageHandler(() => new CustomHttpClientHandler())
            .AddJsonOptions();

            services.AddTransient<AuthorizationHandler>();
            services.AddScoped<FamiliaresContext>();
        }
    }
}
