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

        builder.Property(e => e.ConcurrencyStamp).HasColumnName("concurrency_stamp");
        builder.Property(e => e.EmailConfirmed).HasColumnName("email_confirmed");
        builder.Property(e => e.LockoutEnabled).HasColumnName("lockout_enabled");
        builder.Property(e => e.AccessFailedCount).HasColumnName("access_failed_count");
        builder.Property(e => e.LockoutEnd).HasColumnName("lockout_end");
        builder.Property(e => e.NormalizedEmail).HasColumnName("normalized_email");
        builder.Property(e => e.NormalizedUserName).HasColumnName("normalized_user_name");
        builder.Property(e => e.SecurityStamp).HasColumnName("security_stamp");
        builder.Property(e => e.PhoneNumberConfirmed).HasColumnName("phone_number_confirmed");
        builder.Property(e => e.TwoFactorEnabled).HasColumnName("two_factor_enabled");
        ;
    }
}