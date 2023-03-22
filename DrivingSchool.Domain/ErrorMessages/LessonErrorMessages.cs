namespace DrivingSchool.Domain.ErrorMessages;

public static class LessonErrorMessages
{
    public const string LessonOverlapsAnotherLesson = "Это занятие пересекается с другим уже существующим";
    public const string LessonIsAlreadyTaken = "Это занятие уже занято другим студентом";
    public const string LessonFromThePast = "Текущая дата позже указанной, нельзя проводить занятие в прошлом";
    public const string WrongDuration = "Длительность урока не может превышать 4-х академических часов или быть меньше 1 академического часа";
}