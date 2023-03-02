using DrivingSchool.Domain.Enums;

namespace DrivingSchool.Domain.Services;

public interface IAuthorizationService
{
    public Task<BaseResult> RegisterAsync(User user, string phoneNumber, string email, Roles role);
}