using DrivingSchool.Domain.ErrorMessages;
using DrivingSchool.Domain.Repositories;

namespace DrivingSchool.Domain.Validation;

public class LessonBaseValidator : AbstractValidator<LessonBase>
{
    public LessonBaseValidator(ILessonRepository lessonRepository)
    {
        RuleFor(x => x)
            .MustAsync(async (lesson, _) => !await lessonRepository.CheckLessonOverlappingAsync(lesson))
            .WithMessage(LessonErrorMessages.LessonOverlapsAnotherLesson);
        
        RuleFor(x => x.LessonStartDateTime)
            .Must(x => x >= DateTime.Now)
            .WithMessage(LessonErrorMessages.LessonFromThePast);

        RuleFor(x => x.Duration)
            .InclusiveBetween(TimeSpan.FromMinutes(45), TimeSpan.FromHours(3))
            .WithMessage(LessonErrorMessages.WrongDuration);
    }
}