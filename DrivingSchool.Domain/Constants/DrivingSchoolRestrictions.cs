namespace DrivingSchool.Domain.Constants;

public static class DrivingSchoolRestrictions
{
    public const int MinimumYearsToLearn = 16;
    public static DateTime MaximumBirthDate => DateTime.Now.AddYears(MinimumYearsToLearn * -1);
}