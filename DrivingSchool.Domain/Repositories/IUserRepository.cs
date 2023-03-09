using DrivingSchool.Domain.Enums;

namespace DrivingSchool.Domain.Repositories;

public interface IUserRepository
{
    public Task<DatabaseEntityCreationResult> CreateUserAsync(User user);
    public Task<int> UpdateUserAsync(User user);
    public Task<bool> IsUserExistsByPhoneNumberAsync(string phoneNumber);
    public Task<User> GetUserByLoginAsync(string login);
    public Task<User> GetUserByIdAsync(int id);
    public Task<ListDataResult<User>> ListUsersAsync(int itemCount, int pageNumber, string searchText, string field = UserSortingField.Id, bool desc = false);
}