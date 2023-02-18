using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class IdentityUserEntityConfiguration : IEntityTypeConfiguration<IdentityUser<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUser<int>> builder)
    {
        builder.ToTable("user", "blazor_identity");
        builder.HasKey(x => x.Id).HasName("user_pkey");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.UserName).HasColumnName("username");
        builder.Property(e => e.Email).HasColumnName("email");
        builder.Property(e => e.PasswordHash).HasColumnName("password_hash");
        builder.Property(e => e.PhoneNumber).HasColumnName("phone_number");

        builder
            .Ignore(e => e.ConcurrencyStamp)
            .Ignore(e => e.EmailConfirmed)
            .Ignore(e => e.LockoutEnabled)
            .Ignore(e => e.LockoutEnd)
            .Ignore(e => e.NormalizedEmail)
            .Ignore(e => e.SecurityStamp)
            .Ignore(e => e.AccessFailedCount)
            .Ignore(e => e.NormalizedUserName)
            .Ignore(e => e.PhoneNumberConfirmed)
            .Ignore(e => e.TwoFactorEnabled);
    }
}