using DrivingSchool.Domain.Enums;
using DrivingSchool.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Claim = System.Security.Claims.Claim;

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

    public async Task<DatabaseEntityCreationResult> CreateUserAsync(User user)
    {
        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<BaseResult> UpdateUserAsync(User user, bool emailUpdated)
    {
        if (emailUpdated)
        {
            await ChangeUserEmailAsync(user, user.Identity.Email!);
        }

        var res = await _userRepository.UpdateUserAsync(user);

        return new DatabaseEntityCreationResult { Success = true, CreatedEntityId = res };
    }

    public async Task ChangeUserEmailAsync(User user, string newEmail)
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

    public async Task<ListDataResult<User>> ListUsersAsync(int itemCount, int pageNumber, string searchText = "",
        string field = UserSortingField.Id, bool desc = false)
    {
        return await _userRepository.ListUsersAsync(itemCount, pageNumber, searchText, field, desc);
    }

    public async Task<ListDataResult<UserGeneral>> ListStudentsAsync()
    {
        return await _userRepository.ListStudentsAsync();
    }

    public async Task<ListDataResult<UserGeneral>> ListTeachersAsync()
    {
        return await _userRepository.ListTeachersAsync();
    }

    public async Task<string?> GetUserAvatarAsync(int userId)
    {
        return await _userRepository.GetUserAvatarAsync(userId);
    }

    public async Task<string> GetUserDefaultAvatarAsync(int userId)
    {
        return await _userRepository.GetUserDefaultAvatarAsync(userId);
    }

    public async Task SetUserAvatarAsync(int userId, string fileName)
    {
        await _userRepository.SetUserAvatarAsync(userId, fileName);
    }

    public async Task DeleteAvatarAsync(int userId)
    {
        await _userRepository.DeleteAvatarAsync(userId);
    }

    public async Task<BaseResult> DeleteUserAsync(User context)
    {
        await _userRepository.DeleteUserAsync(context.Id);
        return new BaseResult { Success = true };
    }

    public async Task<bool> IsUserDeletedAsync(int userId)
    {
        return await _userRepository.IsUserDeletedAsync(userId);
    }
}