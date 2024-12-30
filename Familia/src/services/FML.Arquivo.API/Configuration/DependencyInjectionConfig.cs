using Amazon.S3;
using FML.File.API.Data.Repositories;
using FML.File.API.Services;
using FML.File.API.Services.Interfaces;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IAmazonS3Service, AmazonS3Service>();
    }

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IArquivoRepository, ArquivoRepository>();
    }
}
