using System.Security.Claims;
using DrivingSchool.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<IdentityUser<int>> _userManager;

    public UserService(IUserRepository userRepository, UserManager<IdentityUser<int>> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public async Task CreateUserAsync(User user)
    {
        await _userRepository.CreateUserAsync(user);
    }

    public async Task<BaseResult> UpdateUserAsync(User user)
    {
        var existingUser = await _userRepository.GetUserByIdAsync(user.Id);
        if (existingUser.Identity.Email != user.Identity.Email)
        {
            await ChangeUserEmail(user, user.Identity.Email!);
        }
        var res = await _userRepository.UpdateUserAsync(user);

        return new DatabaseEntityCreationResult { Success = true, CreatedEntityId = res };
    }
    
    public async Task ChangeUserEmail(User user, string newEmail)
    {
        var identity = user.Identity;
        await _userManager.SetUserNameAsync(identity, newEmail);
        await _userManager.SetEmailAsync(identity, newEmail);
    }

    public async Task<bool> IsUserExistsByPhoneNumberAsync(string phoneNumber)
    {
        return await _userRepository.IsUserExistsByPhoneNumberAsync(phoneNumber);
    }

    public async Task<User> GetUserByLoginAsync(string login)
    {
        return await _userRepository.GetUserByLoginAsync(login);
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<IEnumerable<Claim>> GetUserClaimsByIdAsync(int id)
    {
        return await _userManager.GetClaimsAsync(new IdentityUser<int> { Id = id });
    }
}