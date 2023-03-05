using System.Linq.Expressions;
using DrivingSchool.Data.Extensions;
using DrivingSchool.Data.Models;
using DrivingSchool.Domain.Enums;
using DrivingSchool.Domain.Exceptions;
using DrivingSchool.Domain.Models;
using DrivingSchool.Domain.Repositories;
using DrivingSchool.Domain.Results;
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
        Context.ChangeTracker.Clear();
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

    public async Task<ListDataResult<User>> ListUsers(int itemCount, int pageNumber, string searchText,
        string field = UserSortingField.Id, bool desc = false)
    {
        var property = GetOrderProperty(field);
        var filtered = Context.Users
            .Where(x => string.Join(" ", x.Name, x.Surname, x.Patronymic).ToLower().Contains(searchText.ToLower()));
        var users = await filtered
            .OrderBy(property, desc)
            .Skip(pageNumber * itemCount)
            .Take(itemCount)
            .ToListAsync();
        var identityIds = users.Select(x => x.IdentityId).ToList();
        var identities = Context.UserIdentities.Where(x => identityIds.Contains(x.Id)).ToList();
        foreach (var user in users)
        {
            user.Identity = identities.Single(x => user.IdentityId == x.Id);
        }

        return new ListDataResult<User>
        {
            Items = users.Select(EntityConverter.ConvertUser),
            TotalItemsCount = filtered.Count()
        };
    }

    private Expression<Func<UserDb, object>> GetOrderProperty(string field)
    {
        return field switch
        {
            UserSortingField.Id => x => x.Id,
            UserSortingField.Name => x => x.Name,
            UserSortingField.Surname => x => x.Surname,
            UserSortingField.Patronymic => x => x.Patronymic,
            _ => throw new ArgumentOutOfRangeException(nameof(field), field, null)
        };
    }
}