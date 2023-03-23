namespace DrivingSchool.Data.Models;

public class StudentLessonDb : Entity
{
    public int StudentId { get; init; }
    public UserDb? Student { get; init; }

    public int TeacherId { get; init; }
    public UserDb? Teacher { get; init; }

    public DateTime Date { get; init; }
    public TimeSpan TimeStart { get; init; }
    public int DurationInMinutes { get; init; }
}