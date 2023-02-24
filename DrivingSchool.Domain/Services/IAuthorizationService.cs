using DrivingSchool.Domain.Enums;
using DrivingSchool.Domain.Results;

namespace DrivingSchool.Domain.Services;

public interface IAuthorizationService
{
    public Task<BaseResult> RegisterAsync(string surname, string name, string patronymic, string phoneNumber,
        string email, Roles role);
}