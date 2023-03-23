using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class IdentityRoleEntityConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
    {
        builder.ToTable("role", "blazor_identity");
        builder.HasKey(x => x.Id).HasName("role_pkey");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.ConcurrencyStamp).HasColumnName("concurrency_stamp");
        builder.Property(e => e.NormalizedName).HasColumnName("normalized_name");
    }
}