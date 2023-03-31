namespace DrivingSchool.Validators.ValidationMessages;

public static class PassportValidatorMessages
{
    public const string WrongSeries = "Неправильная серия паспорта";
    public const string WrongNumber = "Неправильный номер паспорта";
    public const string EmptyIssueDate = "Не заполнено необходимое поле";
    public const string WrongIssuerCode = "Неверное указан код подразделения";
    public const string EmptyPlaceOfBirth = "Не заполнено необходимое поле";
    public const string EmptyIssuedBy = "Не заполнено необходимое поле";
    public const string OverflowedIssuedBy = "Максимальная длина - 150 символов";
    public const string OverflowedPlaceOfBirth = "Максимальная длина - 200 символов";
}