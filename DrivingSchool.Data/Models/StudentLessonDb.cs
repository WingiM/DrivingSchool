namespace DrivingSchool.Data.Models;

public class StudentLessonDb
{
    public int Id { get; init; }
    public int StudentId { get; init; }
    public int TeacherId { get; init; }
    public DateTime Date { get; init; }
    public TimeSpan TimeStart { get; init; }
    public int DurationInMinutes { get; init; }
}