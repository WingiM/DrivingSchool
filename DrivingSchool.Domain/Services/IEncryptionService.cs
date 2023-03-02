namespace DrivingSchool.Domain.Services;

public interface IEncryptionService
{
    public string GeneratePasswordForUser();
}