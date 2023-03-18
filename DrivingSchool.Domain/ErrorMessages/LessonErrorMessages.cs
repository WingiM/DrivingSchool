namespace DrivingSchool.Domain.ErrorMessages;

public static class LessonErrorMessages
{
    public const string LessonOverlapsAnotherLesson = "Это занятие пересекается с другим уже существующим";
    public const string LessonIsAlreadyTaken = "Это занятие уже занято другим студентом";
    public const string LessonFromThePast = "Нельзя записываться в прошлое";
}