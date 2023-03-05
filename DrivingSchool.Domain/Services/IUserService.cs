using System.Security.Claims;
using DrivingSchool.Domain.Enums;

namespace DrivingSchool.Domain.Services;

public interface IUserService
{
    public Task CreateUserAsync(User user);
    public Task<BaseResult> UpdateUserAsync(User user, bool emailUpdated);
    public Task ChangeUserEmail(User user, string newEmail);
    public Task<bool> IsUserExistsByPhoneNumberAsync(string phoneNumber);
    public Task<User> GetUserByLoginAsync(string login);
    public Task<User> GetUserByIdAsync(int id);
    public Task<IEnumerable<Claim>> GetUserClaimsByIdAsync(int id);
    public Task<ListDataResult<User>> ListUsers(int itemCount, int pageNumber, string searchText, string field = UserSortingField.Id, bool desc = false);
}