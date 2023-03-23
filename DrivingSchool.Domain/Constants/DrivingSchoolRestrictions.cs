namespace DrivingSchool.Domain.Constants;

public static class DrivingSchoolRestrictions
{
    public const int MinimumYearsToLearn = 16;
    public static DateTime MaximumBirthDate => DateTime.Now.AddYears(MinimumYearsToLearn * -1);

    public static TimeSpan MinimumLessonLength = TimeSpan.FromMinutes(45);
    public static TimeSpan MaximumLessonLength = TimeSpan.FromMinutes(4 * 45);
}