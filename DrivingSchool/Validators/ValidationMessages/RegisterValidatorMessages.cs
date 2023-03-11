namespace DrivingSchool.Validators.ValidationMessages;

public static class RegisterValidatorMessages
{
    public const string EmailNotEmpty = "Электронная почта не должна быть пустой";
    public const string NotEmail = "Введеная строка не является электронной почтой";
    public const string SelectRole = "Необходимо выбрать роль";
    public const string PhoneNotEmpty = "Номер телефона не должен быть пустым";
    public const string SurnameNotEmpty = "Фамилия обязательна для ввода";
    public const string NameNotEmpty = "Имя обязательно для ввода";
    public const string OnlyCyrillicSymbols = "Для ввода разрешены только символы кириллицы";
    public const string BirthDateNotNull = "Дата рождения не может быть пустой";
    public const string PhoneIsTooSmall = "Номер должен содержать 11 символов";
}