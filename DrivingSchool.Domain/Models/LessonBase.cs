namespace DrivingSchool.Domain.Models;

public abstract class LessonBase
{
    public required int TeacherId { get; init; }
    public UserInitials? TeacherInitials { get; init; }
    public required DateTime Date { get; init; }
    public required TimeSpan TimeStart { get; init; }
    public required TimeSpan Duration { get; init; }

    public DateTime LessonStartDateTime => Date.Add(TimeStart);
    public DateTime LessonEndDateTime => LessonStartDateTime.Add(Duration);
    public DateTimeRange DateTimeRange => new() {TimeStart = LessonStartDateTime, TimeEnd = LessonEndDateTime};

    public bool Overlaps(LessonBase other)
    {
        return other.TeacherId == TeacherId && DateTimeRange.Overlaps(other.DateTimeRange);
    }
}