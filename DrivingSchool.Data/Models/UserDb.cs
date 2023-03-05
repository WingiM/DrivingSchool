using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Data.Models;

public class UserDb
{
    public int Id { get; init; }
    public required string Surname { get; init; }
    public required string Name { get; init; }
    public required string Patronymic { get; init; }
    public DateTime BirthDate { get; init; }
    public int RoleId { get; init; }
    
    public required int IdentityId { get; init; }
    public IdentityUser<int> Identity { get; set; } = null!;
    public PassportDb? Passport { get; init; }
}