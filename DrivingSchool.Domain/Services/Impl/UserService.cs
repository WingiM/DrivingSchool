using DrivingSchool.Domain.Repositories;

namespace DrivingSchool.Domain.Services.Impl;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task CreateUserAsync(User user)
    {
        await _userRepository.CreateUserAsync(user);
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
}