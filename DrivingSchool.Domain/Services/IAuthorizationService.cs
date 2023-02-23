namespace DrivingSchool.Domain.Services;

public interface IAuthorizationService
{
    public Task<bool> RegisterAsync(string surname, string name, string patronymic, string phoneNumber, string email);
}