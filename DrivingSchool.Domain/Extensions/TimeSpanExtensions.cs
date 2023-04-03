namespace DrivingSchool.Domain.Extensions;

public static class TimeSpanExtensions
{
    public static TimeSpan ToAcademicHours(this TimeSpan span)
    {
        return TimeSpan.FromHours(span.TotalHours * 0.75);
    }

    public static bool Between(this TimeSpan span, TimeSpan first, TimeSpan second)
    {
        return span >= first && span <= second;
    }
}