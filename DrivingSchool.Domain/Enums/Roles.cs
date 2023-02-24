using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Domain.Enums;

public enum Roles
{
    [Display(Name = "Administrator")] Administrator = 1,
    [Display(Name = "Student")] Student = 2
}