using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Repositories;

public interface IIdentityCachingRepository
{
    public IdentityUser<int>? FindIdentityById(int id);
    public IdentityUser<int>? FindIdentityByEmail(string email);
    public IdentityUser<int>? FindIdentityByPhone(string phone);
}