using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class IdentityUserClaimsEntityConfiguration : IEntityTypeConfiguration<IdentityUserClaim<int>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<int>> builder)
    {
        builder.ToTable("user_claim", "blazor_identity");

        builder.HasKey(x => x.Id).HasName("user_claim_pkey");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.ClaimType).HasColumnName("claim_type");
        builder.Property(x => x.ClaimValue).HasColumnName("claim_value");
        builder.Property(x => x.UserId).HasColumnName("user_id");
    }
}