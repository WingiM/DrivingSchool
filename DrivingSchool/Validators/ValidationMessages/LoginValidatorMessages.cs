namespace DrivingSchool.Validators.ValidationMessages;

public static class LoginValidatorMessages
{
    public const string EmailNotEmpty = "Электронная почта не должна быть пустой";
    public const string NotEmail = "Введеная строка не является электронной почтой";
    
    public const string PasswordNotEmpty = "Пароль не должна быть пустым";
    public const string TooShortPassword = "Пароль не может содержать менее 5 символов";
}
