using DrivingSchool.Domain.Enums;

namespace DrivingSchool.Data;

public class RegistrationCredentials
{
    public string Surname { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Roles Role { get; set; }
}