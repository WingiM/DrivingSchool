﻿using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Models;

public class User
{
    public int Id { get; init; }
    public required string Surname { get; init; }
    public required string Name { get; init; }
    public required string Patronymic { get; init; }
    public required DateTime BirthDate { get; init; }

    public IdentityUser<int> Identity { get; set; } = null!;
    public Passport? Passport { get; init; }
}