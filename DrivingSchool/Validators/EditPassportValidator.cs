using DrivingSchool.Data;
using FluentValidation;

namespace DrivingSchool.Validators;

public class EditPassportValidator : AbstractValidator<EditPassport>
{
    public EditPassportValidator()
    {
        RuleFor(x => x.Series)
            .Length(4);
        RuleFor(x => x.Number)
            .Length(6);
        RuleFor(x => x.IssueDate)
            .NotEmpty();
        RuleFor(x => x.IssuerCode)
            .Length(6);
        RuleFor(x => x.IssuedBy)
            .NotEmpty()
            .MaximumLength(150);
        RuleFor(x => x.PlaceOfBirth)
            .NotEmpty()
            .MaximumLength(200);
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