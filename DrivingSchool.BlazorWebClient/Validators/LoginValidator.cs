using DrivingSchool.BlazorWebClient.Data;
using DrivingSchool.BlazorWebClient.Validators.ValidationMessages;
using FluentValidation;

namespace DrivingSchool.BlazorWebClient.Validators;

public class LoginValidator : AbstractValidator<LoginCredentials>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(LoginValidatorMessages.EmailNotEmpty)
            .EmailAddress()
            .WithMessage(LoginValidatorMessages.NotEmail);

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(LoginValidatorMessages.PasswordNotEmpty)
            .MinimumLength(5)
            .WithMessage(LoginValidatorMessages.TooShortPassword);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(
                ValidationContext<LoginCredentials>.CreateWithOptions((LoginCredentials)model,
                    x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}