using System.Security.Cryptography;
using DrivingSchool.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Services.Impl;

public class AuthorizationService : IAuthorizationService
{
    private readonly IMailingService _mailingService;
    private readonly UserManager<IdentityUser<int>> _identityManager;
    private readonly IUserRepository _userRepository;
    private readonly UserSecrets _userSecrets;

    public AuthorizationService(UserManager<IdentityUser<int>> identityManager, IUserRepository userRepository,
        UserSecrets userSecrets, IMailingService mailingService)
    {
        _identityManager = identityManager;
        _userRepository = userRepository;
        _userSecrets = userSecrets;
        _mailingService = mailingService;
    }

    public async Task<bool> RegisterAsync(string surname, string name, string patronymic, string phoneNumber,
        string email)
    {
        var identityUser = new IdentityUser<int> { Email = email, UserName = email };

        if (await _identityManager.FindByEmailAsync(email) is not null)
        {
            return false;
        }
        
        var user = new User
        {
            Surname = surname,
            Name = name,
            Patronymic = patronymic,
            Identity = identityUser
        };

        var password = GeneratePasswordForUser();
        var result = await _identityManager.CreateAsync(identityUser, password);
        if (!result.Succeeded) return false;

        await _userRepository.CreateUserAsync(user);
        await _mailingService.SendUserRegisteredMessageAsync(user, password);
        return true;
    }

    private string GeneratePasswordForUser()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars.Concat(chars.ToLower()).ToArray(), _userSecrets.PasswordLength)
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
    }
}