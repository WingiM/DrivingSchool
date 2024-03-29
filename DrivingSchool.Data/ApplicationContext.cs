﻿using DrivingSchool.Data.Configurations;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<UserDb> Users { get; set; } = null!;
    public DbSet<PassportDb> Passports { get; set; } = null!;
    public DbSet<ExamTicketDb> ExamTickets { get; set; } = null!;
    public DbSet<StudentLessonDb> Lessons { get; set; } = null!;
    public DbSet<AvailableLessonDb> AvailableLessons { get; set; } = null!;
    public DbSet<ExamHistoryDb> ExamHistories { get; set; } = null!;
    public DbSet<IdentityRole<int>> Roles { get; set; } = null!;
    public DbSet<IdentityUserRole<int>> UserRoles { get; set; } = null!;
    public DbSet<IdentityUserClaim<int>> UserClaims { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityUserEntityConfiguration).Assembly);
    }
}