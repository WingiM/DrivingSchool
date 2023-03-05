using DrivingSchool.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Models;

public class User
{
    public int Id { get; init; }
    public required string Surname { get; init; }
    public required string Name { get; init; }
    public required string Patronymic { get; init; }
    public required DateTime BirthDate { get; init; }
    public required Roles Role { get; set; }

    public IdentityUser<int> Identity { get; set; } = null!;
    public IdentityInfo IdentityInfo { get; set; } = null!;
    public Passport? Passport { get; init; }
}

public class IdentityInfo
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string EmailVerified { get; set; }
}