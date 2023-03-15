namespace DrivingSchool.Domain.Models;

public class StudentLesson : LessonBase
{
    public int Id { get; init; }
    public required int StudentId { get; init; }
    public UserInitials? StudentInitials { get; init; }
}