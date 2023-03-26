using DrivingSchool.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchool.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IPassportRepository, PassportRepository>();
        services.AddTransient<IExamRepository, ExamRepository>();
        services.AddTransient<ILessonRepository, LessonRepository>();
        services.AddTransient<IIdentityCachingRepository, IdentityCachingRepository>();
        services.AddDbContext<ApplicationContext>(options =>
        {
            options.UseNpgsql(connectionString,
                npgsqlOptions => npgsqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }, ServiceLifetime.Transient);
        services.AddDatabaseDeveloperPageExceptionFilter();
        return services;
    }
}