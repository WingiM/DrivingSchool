namespace DrivingSchool.Domain.Services;

public interface IUserService
{
    public Task CreateUserAsync(User user);
}