namespace DrivingSchool.Domain.ErrorMessages;

public static class PassportErrorMessages
{
    public const string PassportAlreadyExists = "Паспорт с такой серией и номером уже существует";
    public const string UserNotDefined = "Паспорт не принадлежит никакому пользователю";
    public const string UserHasPassword = "У пользователя уже существует паспорт";
}
