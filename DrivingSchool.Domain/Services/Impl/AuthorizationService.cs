using System.Security.Claims;
using System.Security.Cryptography;
using DrivingSchool.Domain.Constants;
using DrivingSchool.Domain.Enums;
using DrivingSchool.Domain.Extensions;
using DrivingSchool.Domain.Results;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Services.Impl;

public class AuthorizationService : IAuthorizationService
{
    private readonly IMailingService _mailingService;
    private readonly UserManager<IdentityUser<int>> _identityManager;
    private readonly IUserService _userService;
    private readonly UserSecrets _userSecrets;

    public AuthorizationService(UserManager<IdentityUser<int>> identityManager, IUserService userService,
        UserSecrets userSecrets, IMailingService mailingService)
    {
        _identityManager = identityManager;
        _userService = userService;
        _userSecrets = userSecrets;
        _mailingService = mailingService;
    }

    public async Task<BaseResult> RegisterAsync(User user, string phoneNumber, string email, Roles role)
    {
        var identityUser = new IdentityUser<int> { Email = email, UserName = email, PhoneNumber = phoneNumber };

        if (await _identityManager.FindByEmailAsync(email) is not null)
        {
            return new BaseResult { Message = ResultMessages.UserWithThisEmailAlreadyExists };
        }

        if (await _userService.IsUserExistsByPhoneNumberAsync(phoneNumber))
        {
            return new BaseResult { Message = ResultMessages.UserWithThisPhoneNumberAlreadyExists };
        }

        user.Identity = identityUser;
        var password = GeneratePasswordForUser();
        var result = await _identityManager.CreateAsync(identityUser, password);
        if (!result.Succeeded) return new BaseResult() { Message = ResultMessages.InternalRegisterError };
        await _identityManager.AddToRoleAsync(identityUser, role.GetDisplayName()!);

        await _userService.CreateUserAsync(user);
        await AddDefaultClaimsToUserAsync(user);
        var mailingResult = await _mailingService.SendUserRegisteredMessageAsync(user, password);
        if (mailingResult)
        {
            var code = await _identityManager.GenerateEmailConfirmationTokenAsync(identityUser);
            await _identityManager.ConfirmEmailAsync(identityUser, code);
        }

        return new BaseResult() { Success = true };
    }

    private async Task AddDefaultClaimsToUserAsync(User user)
    {
        await _identityManager.AddClaimAsync(user.Identity,
            new Claim("avatarLetters", $"{user.Surname[0]}{user.Name[0]}"));
    }

    private string GeneratePasswordForUser()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars.Concat(chars.ToLower()).ToArray(), _userSecrets.PasswordLength)
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
    }
}