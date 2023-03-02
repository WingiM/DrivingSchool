using DrivingSchool.Data.Repositories;
using DrivingSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchool.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPassportRepository, PassportRepository>();
        services.AddDbContext<ApplicationContext>(options => { options.UseNpgsql(connectionString); });
        services.AddDatabaseDeveloperPageExceptionFilter();
        return services;
    }
}