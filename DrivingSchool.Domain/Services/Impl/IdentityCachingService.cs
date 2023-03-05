using DrivingSchool.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Services.Impl;

public static class IdentityCache
{
    public static readonly Dictionary<int, IdentityUser<int>> Cache = new();
}
public class IdentityCachingService : IIdentityCachingService
{
    private readonly IIdentityCachingRepository _userRepository;

    public IdentityCachingService(IIdentityCachingRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task AddIdentity(IdentityUser<int> identityUser)
    {
        IdentityCache.Cache.Add(identityUser.Id, identityUser);
        return Task.CompletedTask;
    }

    public Task<IdentityUser<int>?> GetIdentity(int id)
    {
        var res = IdentityCache.Cache.TryGetValue(id, out var identityUser);
        if (res) return Task.FromResult(identityUser);
        identityUser = _userRepository.FindIdentityById(id);
        if (identityUser is not null)
            IdentityCache.Cache.Add(id, identityUser);
        return Task.FromResult(identityUser);
    }

    public Task<IdentityUser<int>?> GetByEmail(string email)
    {
        IdentityUser<int>? identityUser = IdentityCache.Cache.Values.FirstOrDefault(x => x.Email == email);
        if (identityUser is null)
        {
            identityUser = _userRepository.FindIdentityByEmail(email);
            if (identityUser is not null)
                IdentityCache.Cache.Add(identityUser.Id, identityUser);
        }

        return Task.FromResult(identityUser);
    }

    public Task<IdentityUser<int>?> GetByPhone(string phone)
    {
        IdentityUser<int>? identityUser = IdentityCache.Cache.Values.FirstOrDefault(x => x.Email == phone);
        if (identityUser is null)
        {
            identityUser = _userRepository.FindIdentityByPhone(phone);
            if (identityUser is not null)
                IdentityCache.Cache.Add(identityUser.Id, identityUser);
        }

        return Task.FromResult(identityUser);
    }

    public async Task<IEnumerable<IdentityUser<int>>> GetMultiple(IEnumerable<int> ids)
    {
        var list = new List<IdentityUser<int>>();
        foreach (var id in ids)
        {
            list.Add((await GetIdentity(id))!);
        }

        return list;
    }
}