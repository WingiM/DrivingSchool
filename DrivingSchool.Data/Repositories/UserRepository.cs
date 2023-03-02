using DrivingSchool.Domain.Exceptions;
using DrivingSchool.Domain.Models;
using DrivingSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Data.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task CreateUserAsync(User user)
    {
        await Context.Users.AddAsync(EntityConverter.ConvertUser(user));
        await Context.SaveChangesAsync();
    }

    public async Task<int> UpdateUserAsync(User user)
    {
        var entity = EntityConverter.ConvertUser(user);
        Context.Users.Update(entity);
        await Context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> IsUserExistsByPhoneNumberAsync(string phoneNumber)
    {
        return await Context.UserIdentities.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) is not null;
    }

    public async Task<User> GetUserByLoginAsync(string login)
    {
        var identity = await Context.UserIdentities.SingleOrDefaultAsync(x => x.UserName == login) ??
                       throw new NotFoundException();
        var user = await Context.Users
            .Include(x => x.Passport)
            .SingleAsync(x => x.IdentityId == identity.Id);
        user.Identity = identity;

        return EntityConverter.ConvertUser(user);
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await Context.Users
            .Include(x => x.Passport)
            .SingleOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException();
        var identity = await Context.UserIdentities.SingleAsync(x => x.Id == user.IdentityId);
        user.Identity = identity;

        return EntityConverter.ConvertUser(user);
    }
}