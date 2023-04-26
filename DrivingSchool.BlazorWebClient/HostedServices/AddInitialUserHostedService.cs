using Dapper;
using DrivingSchool.Data;
using DrivingSchool.Domain.Enums;
using DrivingSchool.Domain.Models;
using DrivingSchool.Domain.Services;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.BlazorWebClient.HostedServices;

public class AddInitialUserHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<AddInitialUserHostedService> _logger;

    public AddInitialUserHostedService(IServiceProvider serviceProvider,
        ILogger<AddInitialUserHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var databaseContext = scope.ServiceProvider.GetService<NpgsqlContext>();
        var authorizationService = scope.ServiceProvider.GetService<IAuthorizationService>();
        var userManager = scope.ServiceProvider.GetService<UserManager<IdentityUser<int>>>();
        if (databaseContext is null || authorizationService is null || userManager is null)
            throw new Exception();


        var createUser = await databaseContext.Connection.QuerySingleAsync<bool>("SELECT initial_launch FROM system_info.system_state");
        if (createUser)
        {
            _logger.LogInformation("Skipping default user creation");
            return;
        }

        var user = new User
        {
            Surname = "Админ",
            Name = "Админ",
            Patronymic = "",
            BirthDate = new DateTime(2000, 1, 1),
            Role = Roles.Administrator
        };
        var res = await authorizationService.RegisterAsync(user, "", "admin@admin");
        if (!res.Success)
        {
            _logger.LogCritical("Failed to load default user: {Message}", res.Message);
            throw new Exception();
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user.Identity);
        await userManager.ResetPasswordAsync(user.Identity, token, "admin");
        await databaseContext.Connection.ExecuteAsync("UPDATE system_info.system_state SET initial_launch=true");
        _logger.LogInformation("Default admin user loaded");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}