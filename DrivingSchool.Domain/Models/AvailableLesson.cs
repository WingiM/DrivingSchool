namespace DrivingSchool.Domain.Models;

public class AvailableLesson : LessonBase
{
    public int? StudentId { get; init; }
    public bool IsTaken { get; init; }
}