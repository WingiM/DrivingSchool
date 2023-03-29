namespace DrivingSchool.Domain.Models;

public class DateTimeRange
{
    public required DateTime TimeStart { get; init; }
    public required DateTime TimeEnd { get; init; }

    public bool Overlaps(DateTimeRange other)
    {
        return TimeStart < other.TimeEnd && other.TimeStart < TimeEnd;
    }
}