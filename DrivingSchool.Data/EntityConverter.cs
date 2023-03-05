using DrivingSchool.Data.Models;
using DrivingSchool.Domain.Enums;
using DrivingSchool.Domain.Models;

namespace DrivingSchool.Data;

public static class EntityConverter
{
    public static Passport? ConvertPassport(PassportDb? passportDb)
    {
        return passportDb is null
            ? null
            : new Passport
            {
                Id = passportDb.Id, Number = passportDb.Number, Series = passportDb.Series,
                IssuedBy = passportDb.IssuedBy, IssueDate = passportDb.IssueDate.ToUniversalTime(), 
                IssuerCode = passportDb.IssuerCode,
                PlaceOfBirth = passportDb.PlaceOfBirth, UserId = passportDb.UserId
            };
    }

    public static PassportDb? ConvertPassport(Passport? passport)
    {
        return passport is null
            ? null
            : new PassportDb
            {
                Id = passport.Id, Number = passport.Number, Series = passport.Series,
                IssuedBy = passport.IssuedBy, IssueDate = passport.IssueDate.ToUniversalTime(), 
                IssuerCode = passport.IssuerCode,
                PlaceOfBirth = passport.PlaceOfBirth, UserId = passport.UserId
            };
    }

    public static User ConvertUser(UserDb userDb)
    {
        return new User
        {
            Id = userDb.Id, BirthDate = userDb.BirthDate.ToUniversalTime(), Surname = userDb.Surname,
            Name = userDb.Name, Patronymic = userDb.Patronymic, Passport = ConvertPassport(userDb.Passport),
            Identity = userDb.Identity, Role = (Roles)userDb.RoleId
        };
    }

    public static UserDb ConvertUser(User user)
    {
        return new UserDb
        {
            Id = user.Id,
            BirthDate = user.BirthDate.ToUniversalTime(),
            Surname = user.Surname,
            Name = user.Name,
            Patronymic = user.Patronymic,
            Passport = ConvertPassport(user.Passport),
            Identity = user.Identity,
            IdentityId = user.Identity.Id,
            RoleId = (int)user.Role
        };
    }
}