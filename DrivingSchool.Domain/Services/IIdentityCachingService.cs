using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Services;

public interface IIdentityCachingService
{
    public Task AddIdentity(IdentityUser<int> identityUser);
    public Task<IdentityUser<int>?> GetIdentity(int identityId);
    public Task<IdentityUser<int>?> GetByEmail(string email);
    public Task<IdentityUser<int>?> GetByPhone(string phone);
    public Task<IEnumerable<IdentityUser<int>>> GetMultiple(IEnumerable<int> ids);
}