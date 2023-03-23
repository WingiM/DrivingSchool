using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class PassportEntityConfiguration : IEntityTypeConfiguration<PassportDb>
{
    public void Configure(EntityTypeBuilder<PassportDb> builder)
    {
        builder.ToTable("passport", "public");
        builder.HasKey(x => x.Id).HasName("passport_pkey");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Series).HasColumnName("series");
        builder.Property(x => x.Number).HasColumnName("number");
        builder.Property(x => x.IssueDate).HasColumnName("issue_date");
        builder.Property(x => x.IssuedBy).HasColumnName("issued_by");
        builder.Property(x => x.IssuerCode).HasColumnName("issuer_code");
        builder.Property(x => x.PlaceOfBirth).HasColumnName("place_of_birth");
        builder.Property(x => x.UserId).HasColumnName("user_id");

        builder
            .HasOne(x => x.User)
            .WithOne(x => x.Passport)
            .HasConstraintName("passport_user_id_fkey");
    }
}