using DrivingSchool.Data;
using DrivingSchool.Validators.ValidationMessages;
using FluentValidation;

namespace DrivingSchool.Validators;

public class CreateLessonValidator : AbstractValidator<CreateLesson>
{
    public CreateLessonValidator()
    {
        RuleFor(x => x.StudentId)
            .NotNull()
            .WithMessage(CreateLessonValidatorMessages.StudentNotDefined)
            .GreaterThan(0)
            .WithMessage(CreateLessonValidatorMessages.StudentNotDefined);

        RuleFor(x => x.TeacherId)
            .GreaterThan(0)
            .WithMessage(CreateLessonValidatorMessages.TeacherNotDefined);

        RuleFor(x => x.Date)
            .Must(x => x > DateTime.Now)
            .WithMessage(CreateLessonValidatorMessages.LessonTimePassed);

        RuleFor(x => x.Duration)
            .LessThanOrEqualTo(TimeSpan.FromHours(3))
            .WithMessage(CreateLessonValidatorMessages.WrongDuration)
            .GreaterThan(TimeSpan.Zero)
            .WithMessage(CreateLessonValidatorMessages.WrongDuration);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(
                ValidationContext<CreateLesson>.CreateWithOptions((CreateLesson)model,
                    x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}