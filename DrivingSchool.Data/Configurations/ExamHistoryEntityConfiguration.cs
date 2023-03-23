using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class ExamHistoryEntityConfiguration : IEntityTypeConfiguration<ExamHistoryDb>
{
    public void Configure(EntityTypeBuilder<ExamHistoryDb> builder)
    {
        builder.ToTable("exam_history", "public");
        builder.HasKey(x => x.Id).HasName("exam_history_pkey");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.CorrectAnswers).HasColumnName("correctly_answered_count");
        builder.Property(x => x.WrongAnswers).HasColumnName("wrong_answered_count");
        builder.Property(x => x.TotalTime).HasColumnName("total_time");
        builder.Property(x => x.Date).HasColumnName("date");

        builder.Property(x => x.UserId).HasColumnName("user_id");
        builder.HasOne(x => x.User);
        builder.Property(x => x.TicketId).HasColumnName("ticket_id");
        builder.HasOne(x => x.Ticket);
    }
}