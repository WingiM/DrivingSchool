using DrivingSchool.Domain.FileSystem;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchool.GridFS;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileSystem(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<MongoConnection>(_ =>
            new MongoConnection(configuration["FileSystemSettings:ConnectionString"]!,
                configuration["FileSystemSettings:DatabaseName"]!));
        services.AddScoped<IFileStorage, FileStorage>();

        return services;
    }
}