using DrivingSchool.Domain.ErrorMessages;
using DrivingSchool.Domain.Repositories;

namespace DrivingSchool.Domain.Validation;

public class PassportValidator : AbstractValidator<Passport>
{
    public PassportValidator(IPassportRepository passportRepository)
    {
        RuleFor(x => x.UserId)
            .Must(x => x != 0)
            .WithMessage(PassportErrorMessages.UserNotDefined);

        When(x => x.Id == 0, () =>
        {
            RuleFor(x => x.UserId).MustAsync(async (value, _) =>
                    !await passportRepository.UserHasPassportAsync(value))
                .WithMessage(PassportErrorMessages.UserHasPassword);
        });

        RuleFor(x => new { x.Series, x.Number, x.UserId })
            .MustAsync(async (value, _) =>
                !await passportRepository.SeriesAndPasswordAlreadyExistAsync(value.Series, value.Number, value.UserId))
            .WithMessage(PassportErrorMessages.PassportAlreadyExists);
    }
}