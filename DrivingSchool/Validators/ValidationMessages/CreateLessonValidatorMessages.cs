namespace DrivingSchool.Validators.ValidationMessages;

public static class CreateLessonValidatorMessages
{
    public const string StudentNotDefined = "Необходимо выбрать студента";
    public const string TeacherNotDefined = "Необходимо выбрать учителя";
    public const string LessonTimePassed = "Нельзя записываться в прошлое";
    public const string WrongDuration = "Длительность урока не может превышать 3-х часов";
}