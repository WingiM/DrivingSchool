namespace DrivingSchool.Domain.Services;

public interface IPassportService
{
    public Task<BaseResult> AddOrUpdatePassportAsync(Passport passport);
}