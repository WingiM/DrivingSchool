namespace DrivingSchool.Domain.Extensions;

public static class TimeSpanExtensions
{
    public static TimeSpan ToAcademicHours(this TimeSpan span)
    {
        return TimeSpan.FromHours(span.TotalHours * 0.75);
    }
}