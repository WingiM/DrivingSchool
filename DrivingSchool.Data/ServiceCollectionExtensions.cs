using DrivingSchool.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchool.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPassportRepository, PassportRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
        services.AddScoped<IIdentityCachingRepository, IdentityCachingRepository>();
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(connectionString,
                npgsqlOptions => npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        services.AddDatabaseDeveloperPageExceptionFilter();
        return services;
    }
}