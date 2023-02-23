using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Domain.Enums;

public enum Roles
{
    [Display(Name = "Администратор")] Administrator = 1,
    [Display(Name = "Студент")] Student = 2
}