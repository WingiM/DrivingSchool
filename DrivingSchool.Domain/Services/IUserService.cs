using System.Security.Claims;

namespace DrivingSchool.Domain.Services;

public interface IUserService
{
    public Task CreateUserAsync(User user);
    public Task<BaseResult> UpdateUserAsync(User user);
    public Task<bool> IsUserExistsByPhoneNumberAsync(string phoneNumber);
    public Task<User> GetUserByLoginAsync(string login);
    public Task<User> GetUserByIdAsync(int id);
    public Task<IEnumerable<Claim>> GetUserClaimsByIdAsync(int id);
}