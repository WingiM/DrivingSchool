using DrivingSchool.Data;
using DrivingSchool.Domain.Enums;
using DrivingSchool.Validators.ValidationMessages;
using FluentValidation;

namespace DrivingSchool.Validators;

public class RegistrationValidator : AbstractValidator<RegistrationCredentials>
{
    public RegistrationValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(RegisterValidatorMessages.EmailNotEmpty)
            .EmailAddress()
            .WithMessage(RegisterValidatorMessages.NotEmail);

        RuleFor(x => x.Role)
            .Must(x => Enum.IsDefined(typeof(Roles), x))
            .WithMessage(RegisterValidatorMessages.SelectRole);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(RegisterValidatorMessages.PhoneNotEmpty);

        RuleFor(x => x.Surname)
            .NotEmpty()
            .WithMessage(RegisterValidatorMessages.SurnameNotEmpty)
            .Must(x => x.All(z => z is >= 'А' and <= 'я'))
            .WithMessage(RegisterValidatorMessages.OnlyCyrillicSymbols);      
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(RegisterValidatorMessages.NameNotEmpty)
            .Must(x => x.All(z => z is >= 'А' and <= 'я'))
            .WithMessage(RegisterValidatorMessages.OnlyCyrillicSymbols);

        RuleFor(x => x.Patronymic)
            .Must(x => x.All(z => z is >= 'А' and <= 'я'))
            .WithMessage(RegisterValidatorMessages.OnlyCyrillicSymbols);
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