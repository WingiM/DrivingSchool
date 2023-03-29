using DrivingSchool.Domain.Extensions;

namespace DrivingSchool.Domain.Constants;

public static class DrivingSchoolRestrictions
{
    public const int MinimumYearsToLearn = 16;
    public static DateTime MaximumBirthDate => DateTime.Now.AddYears(MinimumYearsToLearn * -1);

    public static readonly TimeSpan MinimumLessonLength = TimeSpan.FromHours(1).ToAcademicHours();
    public static readonly TimeSpan MaximumLessonLength = TimeSpan.FromHours(4).ToAcademicHours();
}