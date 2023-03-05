using System.Security.Claims;
using DrivingSchool.Domain.Constants;
using DrivingSchool.Domain.Extensions;
using DrivingSchool.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DrivingSchool.Domain.Services.Impl;

public class AuthorizationService : IAuthorizationService
{
    private readonly ILogger<AuthorizationService> _logger;
    private readonly IMailingService _mailingService;
    private readonly IDatabaseAccessRepository _databaseAccessRepository;
    private readonly UserManager<IdentityUser<int>> _identityManager;
    private readonly IUserService _userService;
    private readonly IEncryptionService _encryptionService;

    public AuthorizationService(UserManager<IdentityUser<int>> identityManager, IUserService userService,
        IEncryptionService encryptionService, IMailingService mailingService,
        IDatabaseAccessRepository databaseAccessRepository, ILogger<AuthorizationService> logger)
    {
        _identityManager = identityManager;
        _userService = userService;
        _encryptionService = encryptionService;
        _mailingService = mailingService;
        _databaseAccessRepository = databaseAccessRepository;
        _logger = logger;
    }

    public async Task<BaseResult> RegisterAsync(User user, string phoneNumber, string email,
        bool sendVerificationEmail = false)
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
        if (!result.Succeeded)
        {
            _logger.LogInformation("Registration not completed: {Error}", result);
            return new BaseResult { Message = ResultMessages.InternalRegisterError };
        }
        await _identityManager.AddToRoleAsync(identityUser, user.Role.GetDisplayName()!);

        await _userService.CreateUserAsync(user);
        await AddDefaultClaimsToUserAsync(user);
        if (sendVerificationEmail)
            await VerifyUser(user, password, false);
        await _databaseAccessRepository.ClearTracking();

        return new BaseResult { Success = true };
    }

    public async Task<bool> VerifyUser(User user, string password, bool resetPassword = true)
    {
        var mailingResult = await _mailingService.SendUserRegisteredMessageAsync(user, password);
        if (!mailingResult) return false;
        var code = await _identityManager.GenerateEmailConfirmationTokenAsync(user.Identity);
        await _identityManager.ConfirmEmailAsync(user.Identity, code);
        if (resetPassword)
        {
            var token = await _identityManager.GeneratePasswordResetTokenAsync(user.Identity);
            await _identityManager.ResetPasswordAsync(user.Identity, token, password);
        }

        return true;
    }

    private async Task AddDefaultClaimsToUserAsync(User user)
    {
        await _identityManager.AddClaimAsync(user.Identity,
            new Claim("avatarLetters", $"{user.Surname[0]}{user.Name[0]}".ToUpper()));
    }
}