using System.ComponentModel;
using DrivingSchool.Domain.Enums;

namespace DrivingSchool.Domain.Models;

public class UserGeneral : Entity
{
    [DisplayName("Фамилия")] public required string Surname { get; init; }
    [DisplayName("Имя")] public required string Name { get; init; }
    [DisplayName("Отчество")] public required string Patronymic { get; init; }
    [DisplayName("Дата рождения")] public DateTime? BirthDate { get; init; }
    [DisplayName("Номер телефона")] public string? PhoneNumber { get; init; }
    [DisplayName("Электронная почта")] public string? Email { get; init; }
    [DisplayName("Роль")] public Roles Role { get; init; }

    public override string ToString()
    {
        return $"{Surname} {Name} {Patronymic}";
    }
}