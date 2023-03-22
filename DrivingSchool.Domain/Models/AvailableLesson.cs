namespace DrivingSchool.Domain.Models;

public class AvailableLesson : LessonBase
{
    public int Id { get; init; }
    public int? StudentId { get; init; }
    public bool IsTaken { get; init; }
}