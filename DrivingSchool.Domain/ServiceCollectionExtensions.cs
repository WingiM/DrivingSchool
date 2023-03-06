using DrivingSchool.Domain.Constants;
using DrivingSchool.Domain.Services;
using DrivingSchool.Domain.Services.Impl;
using DrivingSchool.Domain.Validation;
using FluentValidation.AspNetCore;
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
        services.AddScoped<IExamTicketService, ExamTicketService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPassportService, PassportService>();
        services.AddScoped<IIdentityCachingService, IdentityCachingService>();
        services.AddSingleton<IdentityCache>();
        services.AddTransient<IMailingService, MailingService>();

        services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(typeof(PassportValidator).Assembly);
        return services;
    }
}