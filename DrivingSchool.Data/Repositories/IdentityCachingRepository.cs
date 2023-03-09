using DrivingSchool.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Data.Repositories;

public class IdentityCachingRepository : BaseRepository, IIdentityCachingRepository
{
    private readonly DbSet<IdentityUser<int>> _set;
    public IdentityUser<int>? FindIdentityByIdAsync(int id)
    {
        return _set.SingleOrDefault(x => x.Id == id);
    }

    public IdentityUser<int>? FindIdentityByEmailAsync(string email)
    {
        return _set.SingleOrDefault(x => x.Email == email);
    }

    public IdentityUser<int>? FindIdentityByPhoneAsync(string phone)
    {
        return _set.FirstOrDefault(x => x.PhoneNumber == phone);
    }

    public IdentityCachingRepository(ApplicationContext context) : base(context)
    {
        _set = Context.Set<IdentityUser<int>>();
    }
}