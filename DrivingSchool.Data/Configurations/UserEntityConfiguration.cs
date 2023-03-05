using DrivingSchool.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserDb>
{
    public void Configure(EntityTypeBuilder<UserDb> builder)
    {
        builder.ToTable("user", "public");
        builder.HasKey(x => x.Id).HasName("user_pkey");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Surname).HasColumnName("surname");
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.Patronymic).HasColumnName("patronymic");
        builder.Property(x => x.IdentityId).HasColumnName("identity_id");
        builder.Property(x => x.BirthDate).HasColumnName("birth_date");
        builder.Property(x => x.RoleId).HasColumnName("role_id");
    }
}