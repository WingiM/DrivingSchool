using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchool.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ApplicationContext>(options => { options.UseNpgsql(connectionString); });
        services.AddDatabaseDeveloperPageExceptionFilter();
        return services;
    }
}