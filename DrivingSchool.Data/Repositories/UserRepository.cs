using DrivingSchool.Data.Models;
using DrivingSchool.Domain.Models;
using DrivingSchool.Domain.Repositories;

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
}