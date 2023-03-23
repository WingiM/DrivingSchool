using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class IdentityRoleClaimEntityConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<int>> builder)
    {
        builder.ToTable("role_claim", "blazor_identity");

        builder.HasKey(x => x.Id).HasName("role_claim_pkey");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ClaimType).HasColumnName("claim_type");
        builder.Property(x => x.ClaimValue).HasColumnName("claim_value");
        builder.Property(x => x.RoleId).HasColumnName("role_id");
    }
}