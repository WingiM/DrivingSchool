using DrivingSchool.Data.Models;
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
                IssuedBy = passportDb.IssuedBy, IssueDate = passportDb.IssueDate, IssuerCode = passportDb.IssuerCode,
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
                IssuedBy = passport.IssuedBy, IssueDate = passport.IssueDate, IssuerCode = passport.IssuerCode,
                PlaceOfBirth = passport.PlaceOfBirth, UserId = passport.UserId
            };
    }

    public static User ConvertUser(UserDb userDb)
    {
        return new User
        {
            Id = userDb.Id, BirthDate = userDb.BirthDate, Surname = userDb.Surname,
            Name = userDb.Name, Patronymic = userDb.Patronymic, Passport = ConvertPassport(userDb.Passport),
            Identity = userDb.Identity
        };
    }
}