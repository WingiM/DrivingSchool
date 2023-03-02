﻿using DrivingSchool.Data;
using DrivingSchool.Validators.ValidationMessages;
using FluentValidation;

namespace DrivingSchool.Validators;

public class EditUserValidator : AbstractValidator<EditUser>
{
    public EditUserValidator()
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(RegisterValidatorMessages.EmailNotEmpty)
            .EmailAddress()
            .WithMessage(RegisterValidatorMessages.NotEmail);

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

        RuleFor(x => x.BirthDate)
            .NotNull()
            .WithMessage(RegisterValidatorMessages.BirthDateNotNull);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(
                ValidationContext<EditUser>.CreateWithOptions((EditUser)model,
                    x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}