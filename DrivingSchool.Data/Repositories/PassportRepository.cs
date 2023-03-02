using DrivingSchool.Domain.Models;
using DrivingSchool.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Data.Repositories;

public class PassportRepository : BaseRepository, IPassportRepository
{
    public PassportRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<int> AddOrUpdatePassportAsync(Passport passport)
    {
        var entity = EntityConverter.ConvertPassport(passport)!;
        Context.Passports.Update(entity);
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
        return entity.Id;
    }

    public async Task<bool> SeriesAndPasswordAlreadyExist(string series, string number, int userId)
    {
        var passport = await Context.Passports.SingleOrDefaultAsync(x => x.UserId != userId && x.Series == series && x.Number == number);
        return passport is not null;
    }

    public async Task<bool> UserHasPassportAsync(int userId)
    {
        var passport = await Context.Passports.SingleOrDefaultAsync(x => x.UserId == userId);
        return passport is not null;
    }
}