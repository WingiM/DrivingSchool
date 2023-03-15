namespace DrivingSchool.Domain.Models;

public abstract class LessonBase
{
    public required int TeacherId { get; init; }
    public UserInitials? TeacherInitials { get; init; }
    public required DateTime Date { get; init; }
    public required TimeSpan TimeStart { get; init; }
    public required TimeSpan Duration { get; init; }

    public bool Overlaps(LessonBase other)
    {
        var timeEnd = TimeStart.Add(Duration);
        var otherTimeEnd = other.TimeStart.Add(other.Duration);
        return other.TeacherId == TeacherId && other.Date == Date && TimeStart < otherTimeEnd &&
               other.TimeStart < timeEnd;
    }
}