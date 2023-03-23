using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class ExamTicketEntityConfiguration : IEntityTypeConfiguration<ExamTicketDb>
{
    public void Configure(EntityTypeBuilder<ExamTicketDb> builder)
    {
        builder.ToTable("exam_ticket", "public");
        builder.HasKey(x => x.Id).HasName("exam_ticket_pkey1");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Number).HasColumnName("number");

        builder
            .HasMany(x => x.Questions)
            .WithOne(x => x.Ticket);
    }
}