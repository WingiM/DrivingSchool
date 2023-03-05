using DrivingSchool.Domain.Enums;

namespace DrivingSchool.Data;

public class RegistrationCredentials
{
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public Roles Role { get; set; }
    public bool SendVerificationEmail { get; set; }
}