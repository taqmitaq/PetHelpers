using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using PetHelpers.Application.Database;
using PetHelpers.Application.Files;
using PetHelpers.Application.Messaging;
using PetHelpers.Application.Species;
using PetHelpers.Application.Volunteers;
using PetHelpers.Infrastructure.BackgroundServices;
using PetHelpers.Infrastructure.Files;
using PetHelpers.Infrastructure.MessageQueues;
using PetHelpers.Infrastructure.Options;
using PetHelpers.Infrastructure.Providers;
using PetHelpers.Infrastructure.Repositories;
using FileInfo = PetHelpers.Application.Files.FileInfo;

namespace PetHelpers.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ApplicationDbContext>();
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IFileCleanerService, FileCleanerService>();
        services.AddSingleton<IMessageQueue<IEnumerable<FileInfo>>, InMemoryMessageQueue<IEnumerable<FileInfo>>>();
        services.AddHostedService<FileCleanerBackgroundService>();

        services.AddMinio(configuration);

        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(
            configuration.GetSection(MinioOptions.MINIO));

        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>()
                               ?? throw new ApplicationException("Missing minio configuration");

            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.WithSSL);
        });

        services.AddScoped<IFileProvider, MinioProvider>();

        return services;
    }
}