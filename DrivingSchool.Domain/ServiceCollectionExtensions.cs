using DrivingSchool.Domain.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchool.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, string connectionString)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(typeof(UserValidator).Assembly);
        return services;
    }
}