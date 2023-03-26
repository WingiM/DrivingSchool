namespace DrivingSchool.Domain.Constants;

public static class ResultMessages
{
    public const string UserWithThisEmailAlreadyExists = "Пользователь с таким Email уже существует";

    public const string InternalRegisterError =
        "Ошибка сервера при регистрации пользователя. Свяжитесь с администратором.";

    public const string UserWithThisPhoneNumberAlreadyExists = "Пользователь с таким номером телефона уже существует";
}