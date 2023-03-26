using System.Linq.Expressions;
using Dapper;
using DrivingSchool.Data.Extensions;
using DrivingSchool.Domain.Constants;
using DrivingSchool.Domain.Enums;
using DrivingSchool.Domain.Exceptions;
using DrivingSchool.Domain.Services;

namespace DrivingSchool.Data.Repositories;

public class UserRepository : BaseRepository, IUserRepository
{
    private readonly IIdentityCachingService _identityCachingService;

    public UserRepository(ApplicationContext context, NpgsqlContext connection,
        IIdentityCachingService identityCachingService) : base(context, connection)
    {
        _identityCachingService = identityCachingService;
    }

    public async Task<DatabaseEntityCreationResult> CreateUserAsync(User user)
    {
        var entity = EntityConverter.ConvertUser(user);
        await Context.Users.AddAsync(entity);
        await Context.SaveChangesAsync();
        Context.Entry(entity).State = EntityState.Detached;
        return new DatabaseEntityCreationResult { Success = true, CreatedEntityId = entity.Id };
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
        return await _identityCachingService.GetByPhoneAsync(phoneNumber) is not null;
    }

    public async Task<User> GetUserByLoginAsync(string login)
    {
        var identity = (await _identityCachingService.GetByEmailAsync(login))!;
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
        var identity = await _identityCachingService.GetIdentityAsync(user.IdentityId);
        user.Identity = identity!;

        return EntityConverter.ConvertUser(user);
    }

    public async Task<ListDataResult<User>> ListUsersAsync(int itemCount, int pageNumber, string searchText = "",
        string field = UserSortingField.Id, bool desc = false)
    {
        var property = GetOrderProperty(field);
        var filtered = Context.Users
            .Where(x => !x.IsDeleted && string.Join(" ", x.Name, x.Surname, x.Patronymic).ToLower()
                .Contains(searchText.ToLower()));
        var users = await filtered
            .OrderBy(property, desc)
            .Skip(pageNumber * itemCount)
            .Take(itemCount)
            .ToListAsync();
        var identityIds = users.Select(x => x.IdentityId).ToList();
        var identities = (await _identityCachingService.GetMultipleAsync(identityIds)).ToList();
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

    public async Task<ListDataResult<User>> ListStudentsAsync(int itemCount, int pageNumber)
    {
        var filtered = Context.Users
            .Where(x => !x.IsDeleted && x.RoleId == (int)Roles.Student);
        var users = await filtered
            .OrderBy(x => x.Id)
            .Skip(pageNumber * itemCount)
            .Take(itemCount)
            .ToListAsync();
        var identityIds = users.Select(x => x.IdentityId).ToList();
        var identities = (await _identityCachingService.GetMultipleAsync(identityIds)).ToList();
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

    public async Task<ListDataResult<UserGeneral>> ListStudentsAsync()
    {
        var res = Context.Users.Where(x => !x.IsDeleted && x.RoleId == (int)Roles.Student);
        return new ListDataResult<UserGeneral>
        {
            Success = true,
            Items = await res
                .Select(x => EntityConverter.GetUserInitials(x))
                .ToArrayAsync()
        };
    }

    public async Task<ListDataResult<UserGeneral>> ListTeachersAsync()
    {
        var res = Context.Users.Where(x => !x.IsDeleted && x.RoleId == (int)Roles.Teacher);
        return new ListDataResult<UserGeneral>
        {
            Success = true,
            Items = await res
                .Select(x => EntityConverter.GetUserInitials(x))
                .ToArrayAsync()
        };
    }

    public async Task SetUserAvatarAsync(int userId, string fileName)
    {
        var sql = "UPDATE public.user SET avatar = @fileName WHERE id = @id";
        await Connection.ExecuteAsync(sql, new { id = userId, fileName });
    }

    public async Task<string?> GetUserAvatarAsync(int userId)
    {
        var sql = "SELECT avatar FROM public.user WHERE id = @id";
        return await Connection.QueryFirstOrDefaultAsync<string>(sql, new { id = userId });
    }

    public async Task<string> GetUserDefaultAvatarAsync(int userId)
    {
        var sql =
            "SELECT claim_value FROM blazor_identity.user_claim WHERE user_id = (SELECT identity_id FROM public.user WHERE id=@id) AND claim_type = @claimType";
        return await Connection.QuerySingleAsync<string>(sql,
            new { id = userId, claimType = UserDefaultClaims.AvatarLetters });
    }

    public async Task DeleteAvatarAsync(int userId)
    {
        var sql = "UPDATE public.user SET avatar = null WHERE id = @id";
        await Connection.ExecuteAsync(sql, new { id = userId });
    }

    public async Task DeleteUserAsync(int userId)
    {
        await Connection.ExecuteAsync("UPDATE public.user SET is_deleted = true WHERE id = @id", new { id = userId });
    }

    public async Task<bool> IsUserDeletedAsync(int userId)
    {
        return await Connection.QuerySingleAsync<bool>("SELECT is_deleted FROM public.user WHERE id = @id", new {id = userId});
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