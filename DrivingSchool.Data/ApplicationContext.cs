using DrivingSchool.Data.Configurations;
using DrivingSchool.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<UserDb> Users { get; set; } = null!;
    public DbSet<PassportDb> Passports { get; set; } = null!;
    public DbSet<IdentityUser<int>> UserIdentities { get; set; } = null!;
    public DbSet<IdentityRole<int>> Roles { get; set; } = null!;
    public DbSet<IdentityUserRole<int>> UserRoles { get; set; } = null!;
    public DbSet<IdentityUserClaim<int>> UserClaims { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityUserEntityConfiguration).Assembly);
    }
}