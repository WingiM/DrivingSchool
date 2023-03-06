using System.Collections.Concurrent;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Constants;

public class IdentityCache
{
    private readonly ConcurrentDictionary<int, IdentityUser<int>> _cache = new();

    public bool TryGetValue(int key, out IdentityUser<int>? identityUser)
    {
        return _cache.TryGetValue(key, out identityUser);
    }

    public IdentityUser<int>? TryGetValueByEmail(string email)
    {
        return _cache.Values.FirstOrDefault(x => x.Email == email);
    }

    public IdentityUser<int>? TryGetValueByPhone(string phone)
    {
        return _cache.Values.FirstOrDefault(x => x.PhoneNumber == phone);
    }

    public void TryAdd(int key, IdentityUser<int> identityUser)
    {
        _cache.TryAdd(key, identityUser);
    }
}