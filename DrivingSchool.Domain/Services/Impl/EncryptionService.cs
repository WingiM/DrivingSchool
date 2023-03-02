using System.Security.Cryptography;

namespace DrivingSchool.Domain.Services.Impl;

public class EncryptionService : IEncryptionService
{
    private readonly UserSecrets _userSecrets;

    public EncryptionService(UserSecrets userSecrets)
    {
        _userSecrets = userSecrets;
    }

    public string GeneratePasswordForUser()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars.Concat(chars.ToLower()).ToArray(), _userSecrets.PasswordLength)
            .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
    }
}