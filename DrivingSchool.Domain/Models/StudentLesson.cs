namespace DrivingSchool.Domain.Models;

public class StudentLesson : LessonBase
{
    public required int StudentId { get; init; }
    public UserInitials? StudentInitials { get; init; }
}