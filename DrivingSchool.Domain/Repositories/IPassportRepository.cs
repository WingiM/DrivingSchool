namespace DrivingSchool.Domain.Repositories;

public interface IPassportRepository
{
    public Task<int> AddOrUpdatePassportAsync(Passport passport);
    public Task<bool> SeriesAndPasswordAlreadyExistAsync(string series, string number, int userId);
    public Task<bool> UserHasPassportAsync(int userId);
}