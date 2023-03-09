using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Repositories;

public interface IIdentityCachingRepository
{
    public IdentityUser<int>? FindIdentityByIdAsync(int id);
    public IdentityUser<int>? FindIdentityByEmailAsync(string email);
    public IdentityUser<int>? FindIdentityByPhoneAsync(string phone);
}