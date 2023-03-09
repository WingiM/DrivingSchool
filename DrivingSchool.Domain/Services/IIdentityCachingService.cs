using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Services;

public interface IIdentityCachingService
{
    public Task AddIdentityAsync(IdentityUser<int> identityUser);
    public Task<IdentityUser<int>?> GetIdentityAsync(int identityId);
    public Task<IdentityUser<int>?> GetByEmailAsync(string email);
    public Task<IdentityUser<int>?> GetByPhoneAsync(string phone);
    public Task<IEnumerable<IdentityUser<int>>> GetMultipleAsync(IEnumerable<int> ids);
}