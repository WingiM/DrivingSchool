using DrivingSchool.Domain.Constants;
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
            .InclusiveBetween(DrivingSchoolRestrictions.MinimumLessonLength, DrivingSchoolRestrictions.MaximumLessonLength)
            .WithMessage(LessonErrorMessages.WrongDuration);
    }
}