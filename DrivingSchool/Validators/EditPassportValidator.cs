using DrivingSchool.Data;
using DrivingSchool.Validators.ValidationMessages;
using FluentValidation;

namespace DrivingSchool.Validators;

public class EditPassportValidator : AbstractValidator<EditPassport>
{
    public EditPassportValidator()
    {
        RuleFor(x => x.Series)
            .Length(4)
            .WithMessage(PassportValidatorMessages.WrongSeries);
        RuleFor(x => x.Number)
            .Length(6)
            .WithMessage(PassportValidatorMessages.WrongNumber);
        RuleFor(x => x.IssueDate)
            .NotEmpty()
            .WithMessage(PassportValidatorMessages.EmptyIssueDate);
        RuleFor(x => x.IssuerCode)
            .Length(6)
            .WithMessage(PassportValidatorMessages.WrongIssuerCode);
        RuleFor(x => x.IssuedBy)
            .NotEmpty()
            .WithMessage(PassportValidatorMessages.EmptyIssuedBy)
            .MaximumLength(150)
            .WithMessage(PassportValidatorMessages.OverflowedIssuedBy);
        RuleFor(x => x.PlaceOfBirth)
            .NotEmpty()
            .WithMessage(PassportValidatorMessages.EmptyPlaceOfBirth)
            .MaximumLength(200)
            .WithMessage(PassportValidatorMessages.OverflowedPlaceOfBirth);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(
                ValidationContext<EditPassport>.CreateWithOptions((EditPassport)model,
                    x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}