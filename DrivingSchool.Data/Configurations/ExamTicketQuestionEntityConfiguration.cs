using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class ExamTicketQuestionEntityConfiguration : IEntityTypeConfiguration<ExamTicketQuestionDb>
{
    public void Configure(EntityTypeBuilder<ExamTicketQuestionDb> builder)
    {
        builder.ToTable("exam_ticket_question", "public");
        builder.HasKey(x => x.Id).HasName("exam_ticket_pkey");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.Question).HasColumnName("question");
        builder.Property(x => x.ImageSource).HasColumnName("image_source");
        builder.Property(x => x.Comment).HasColumnName("comment");
        builder.Property(x => x.NumberInTicket).HasColumnName("number_in_ticket");
        builder.Property(x => x.TicketId).HasColumnName("ticket_id");

        builder
            .HasMany(x => x.Answers)
            .WithOne(x => x.Question);
    }
}