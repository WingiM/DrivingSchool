namespace DrivingSchool.Domain.Repositories;

public interface IUserRepository
{
    public Task CreateUserAsync(User user);
    public Task<int> UpdateUserAsync(User user);
    public Task<bool> IsUserExistsByPhoneNumberAsync(string phoneNumber);
    public Task<User> GetUserByLoginAsync(string login);
    public Task<User> GetUserByIdAsync(int id);
}