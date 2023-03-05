using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Domain.Constants;

public class IdentityCache
{
    public readonly Dictionary<int, IdentityUser<int>> Cache = new();
}