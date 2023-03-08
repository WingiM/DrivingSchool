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
        services.AddScoped<IDatabaseAccessRepository, DatabaseAccessRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IIdentityCachingRepository, IdentityCachingRepository>();
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        services.AddDatabaseDeveloperPageExceptionFilter();
        return services;
    }
}