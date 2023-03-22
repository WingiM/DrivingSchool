using DrivingSchool.Data;
using DrivingSchool.Validators.ValidationMessages;
using FluentValidation;

namespace DrivingSchool.Validators;

public class CreateLessonValidator : AbstractValidator<CreateLesson>
{
    public CreateLessonValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0)
            .WithMessage(CreateLessonValidatorMessages.StudentNotDefined);

        RuleFor(x => x.TeacherId)
            .GreaterThan(0)
            .WithMessage(CreateLessonValidatorMessages.TeacherNotDefined);

        RuleFor(x => x.Date)
            .NotNull()
            .WithMessage(CreateLessonValidatorMessages.DateNotDefined)
            .Must(x => x >= DateTime.Now.Date)
            .WithMessage(CreateLessonValidatorMessages.LessonTimePassed);

        RuleFor(x => x.TimeStart)
            .NotNull()
            .WithMessage(CreateLessonValidatorMessages.WrongDuration);
        
        RuleFor(x => x.TimeEnd)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(CreateLessonValidatorMessages.WrongDuration)
            .Must((lesson, span) => span!.Value - lesson.TimeStart >= TimeSpan.FromMinutes(45)
                                    && span.Value - lesson.TimeStart <= TimeSpan.FromHours(3))
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