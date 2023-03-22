namespace DrivingSchool.Domain.Models;

public class DateTimeRange
{
    public required DateTime TimeStart { get; set; }
    public required DateTime TimeEnd { get; set; }

    public bool Overlaps(DateTimeRange other)
    {
        return TimeStart < other.TimeEnd && other.TimeStart < TimeEnd;
    }
}