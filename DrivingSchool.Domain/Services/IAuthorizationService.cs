namespace DrivingSchool.Domain.Services;

public interface IAuthorizationService
{
    public Task<BaseResult> RegisterAsync(User user, string phoneNumber, string email, bool sendVerificationEmail = false);
    public Task<bool> VerifyUserAsync(User user, string password);
}