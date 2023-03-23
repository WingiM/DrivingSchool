using DrivingSchool.Domain.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Data.Models;

public class UserDb : Entity
{
    public required string Surname { get; init; }
    public required string Name { get; init; }
    public required string Patronymic { get; init; }
    public DateTime BirthDate { get; init; }
    public int RoleId { get; init; }
    
    public required int IdentityId { get; init; }
    public IdentityUser<int> Identity { get; set; } = null!;
    public PassportDb? Passport { get; init; }
    public List<StudentLessonDb>? LessonsStudent { get; init; }
    public List<StudentLessonDb>? Lessons { get; init; }
    public List<AvailableLessonDb>? AvailableLessonsStudent { get; init; }
    public List<AvailableLessonDb>? AvailableLessons { get; init; }
}