namespace DrivingSchool.Domain.Repositories;

public interface IUserRepository
{
    public Task CreateUserAsync(User user);
}