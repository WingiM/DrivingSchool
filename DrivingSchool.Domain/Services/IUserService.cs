using System.Security.Claims;
using DrivingSchool.Domain.Enums;

namespace DrivingSchool.Domain.Services;

public interface IUserService
{
    public Task<DatabaseEntityCreationResult> CreateUserAsync(User user);
    public Task<BaseResult> UpdateUserAsync(User user, bool emailUpdated);
    public Task ChangeUserEmailAsync(User user, string newEmail);
    public Task<bool> IsUserExistsByPhoneNumberAsync(string phoneNumber);
    public Task<User> GetUserByLoginAsync(string login);
    public Task<User> GetUserByIdAsync(int id);
    public Task<IEnumerable<Claim>> GetUserClaimsByIdAsync(int id);
    public Task<ListDataResult<User>> ListUsersAsync(int itemCount, int pageNumber, string searchText = "", string field = UserSortingField.Id, bool desc = false);
    public Task<ListDataResult<UserGeneral>> ListStudentsAsync();
    public Task<ListDataResult<UserGeneral>> ListTeachersAsync();
    public Task<string?> GetUserAvatarAsync(int userId);
    public Task<string> GetUserDefaultAvatarAsync(int userId);
    public Task SetUserAvatarAsync(int userId, string fileName);
    public Task DeleteAvatarAsync(int userId);
    public Task<BaseResult> DeleteUserAsync(User context);
}