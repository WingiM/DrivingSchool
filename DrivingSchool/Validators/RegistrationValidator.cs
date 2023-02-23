using DrivingSchool.Data;
using FluentValidation;

namespace DrivingSchool.Validators;

public class RegistrationValidator : AbstractValidator<RegistrationCredentials>
{
    public RegistrationValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(
                ValidationContext<RegistrationCredentials>.CreateWithOptions((RegistrationCredentials)model,
                    x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}