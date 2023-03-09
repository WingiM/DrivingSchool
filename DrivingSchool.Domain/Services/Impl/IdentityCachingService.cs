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

    public Task AddIdentityAsync(IdentityUser<int> identityUser)
    {
        _cache.TryAdd(identityUser.Id, identityUser);
        return Task.CompletedTask;
    }

    public Task<IdentityUser<int>?> GetIdentityAsync(int id)
    {
        var res = _cache.TryGetValue(id, out var identityUser);
        if (res) return Task.FromResult(identityUser);
        identityUser = _identityCachingRepository.FindIdentityByIdAsync(id);
        if (identityUser is not null)
            _cache.TryAdd(id, identityUser);
        return Task.FromResult(identityUser);
    }

    public Task<IdentityUser<int>?> GetByEmailAsync(string email)
    {
        var identityUser = _cache.TryGetValueByEmail(email);
        if (identityUser is null)
        {
            identityUser = _identityCachingRepository.FindIdentityByEmailAsync(email);
            if (identityUser is not null)
                _cache.TryAdd(identityUser.Id, identityUser);
        }

        return Task.FromResult(identityUser);
    }

    public Task<IdentityUser<int>?> GetByPhoneAsync(string phone)
    {
        IdentityUser<int>? identityUser = _cache.TryGetValueByPhone(phone);
        if (identityUser is null)
        {
            identityUser = _identityCachingRepository.FindIdentityByPhoneAsync(phone);
            if (identityUser is not null)
                _cache.TryAdd(identityUser.Id, identityUser);
        }

        return Task.FromResult(identityUser);
    }

    public async Task<IEnumerable<IdentityUser<int>>> GetMultipleAsync(IEnumerable<int> ids)
    {
        var list = new List<IdentityUser<int>>();
        foreach (var id in ids)
        {
            list.Add((await GetIdentityAsync(id))!);
        }

        return list;
    }
}