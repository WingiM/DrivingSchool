using System.Security.Claims;
using DrivingSchool.Domain.Constants;
using DrivingSchool.Domain.Enums;
using DrivingSchool.Domain.Extensions;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Services.Impl;

public class AuthorizationService : IAuthorizationService
{
    private readonly IMailingService _mailingService;
    private readonly UserManager<IdentityUser<int>> _identityManager;
    private readonly IUserService _userService;
    private readonly IEncryptionService _encryptionService;

    public AuthorizationService(UserManager<IdentityUser<int>> identityManager, IUserService userService,
        IEncryptionService encryptionService, IMailingService mailingService)
    {
        _identityManager = identityManager;
        _userService = userService;
        _encryptionService = encryptionService;
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
        var password = _encryptionService.GeneratePasswordForUser();
        var result = await _identityManager.CreateAsync(identityUser, password);
        if (!result.Succeeded) return new BaseResult { Message = ResultMessages.InternalRegisterError };
        await _identityManager.AddToRoleAsync(identityUser, role.GetDisplayName()!);

        await _userService.CreateUserAsync(user);
        await AddDefaultClaimsToUserAsync(user);
        await VerifyUser(user, password);

        return new BaseResult { Success = true };
    }

    public async Task<bool> VerifyUser(User user, string password)
    {
        var mailingResult = await _mailingService.SendUserRegisteredMessageAsync(user, password);
        if (!mailingResult) return false;
        var code = await _identityManager.GenerateEmailConfirmationTokenAsync(user.Identity);
        await _identityManager.ConfirmEmailAsync(user.Identity, code);
        var token = await _identityManager.GeneratePasswordResetTokenAsync(user.Identity);
        await _identityManager.ResetPasswordAsync(user.Identity, token, password);
        return true;
    }

    private async Task AddDefaultClaimsToUserAsync(User user)
    {
        await _identityManager.AddClaimAsync(user.Identity,
            new Claim("avatarLetters", $"{user.Surname[0]}{user.Name[0]}".ToUpper()));
    }
}