using DrivingSchool.Domain.Services;
using DrivingSchool.Domain.Services.Impl;
using DrivingSchool.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrivingSchool.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        var userSecrets = new UserSecrets();
        var mailSettings = new MailSettings();
        configuration.Bind(nameof(userSecrets), userSecrets);
        configuration.Bind(nameof(mailSettings), mailSettings);
        services.AddSingleton(userSecrets);
        services.AddSingleton(mailSettings);

        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddTransient<IMailingService, MailingService>();
        return services;
    }
}