using DrivingSchool.Domain.Constants;
using DrivingSchool.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Services.Impl;

public class IdentityCachingService : IIdentityCachingService
{
    private readonly IdentityCache _cache;
    private readonly IIdentityCachingRepository _identityCachingRepository;

    public IdentityCachingService(IIdentityCachingRepository identityCachingRepository, IdentityCache cache)
    {
        _identityCachingRepository = identityCachingRepository;
        _cache = cache;
    }

    public Task AddIdentity(IdentityUser<int> identityUser)
    {
        _cache.TryAdd(identityUser.Id, identityUser);
        return Task.CompletedTask;
    }

    public Task<IdentityUser<int>?> GetIdentity(int id)
    {
        var res = _cache.TryGetValue(id, out var identityUser);
        if (res) return Task.FromResult(identityUser);
        identityUser = _identityCachingRepository.FindIdentityById(id);
        if (identityUser is not null)
            _cache.TryAdd(id, identityUser);
        return Task.FromResult(identityUser);
    }

    public Task<IdentityUser<int>?> GetByEmail(string email)
    {
        var identityUser = _cache.TryGetValueByEmail(email);
        if (identityUser is null)
        {
            identityUser = _identityCachingRepository.FindIdentityByEmail(email);
            if (identityUser is not null)
                _cache.TryAdd(identityUser.Id, identityUser);
        }

        return Task.FromResult(identityUser);
    }

    public Task<IdentityUser<int>?> GetByPhone(string phone)
    {
        IdentityUser<int>? identityUser = _cache.TryGetValueByPhone(phone);
        if (identityUser is null)
        {
            identityUser = _identityCachingRepository.FindIdentityByPhone(phone);
            if (identityUser is not null)
                _cache.TryAdd(identityUser.Id, identityUser);
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