using DrivingSchool.Data.Models;
using DrivingSchool.Domain.Exceptions;
using DrivingSchool.Domain.Models;
using DrivingSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(User user)
    {
        var userDb = new UserDb
        {
            Surname = user.Surname,
            Name = user.Name,
            Patronymic = user.Patronymic,
            IdentityId = user.Identity.Id
        };

        await _context.Users.AddAsync(userDb);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsUserExistsByPhoneNumberAsync(string phoneNumber)
    {
        return await _context.UserIdentities.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) is not null;
    }

    public async Task<User> GetUserByLoginAsync(string login)
    {
        var identity = await _context.UserIdentities.SingleOrDefaultAsync(x => x.UserName == login) ??
                       throw new NotFoundException();
        var user = await _context.Users.SingleAsync(x => x.IdentityId == identity.Id);

        return new User
        {
            Id = user.Id,
            Surname = user.Surname,
            Name = user.Name,
            Patronymic = user.Patronymic,
            Identity = identity
        };
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException();
        var identity = await _context.UserIdentities.SingleAsync(x => x.Id == user.IdentityId);

        return new User
        {
            Id = user.Id,
            Surname = user.Surname,
            Name = user.Name,
            Patronymic = user.Patronymic,
            Identity = identity
        };
    }
}