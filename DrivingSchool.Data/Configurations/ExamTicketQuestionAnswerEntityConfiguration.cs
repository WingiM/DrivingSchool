using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class ExamTicketQuestionAnswerEntityConfiguration : IEntityTypeConfiguration<ExamTicketQuestionAnswerDb>
{
    public void Configure(EntityTypeBuilder<ExamTicketQuestionAnswerDb> builder)
    {
        builder.ToTable("exam_ticket_answer", "public");
        builder.HasKey(x => x.Id).HasName("exam_ticket_answer_pkey");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.NumberInQuestion).HasColumnName("number_in_question");
        builder.Property(x => x.AnswerText).HasColumnName("answer_text");
        builder.Property(x => x.IsCorrect).HasColumnName("is_correct");
        builder.Property(x => x.QuestionId).HasColumnName("question_id");
    }
}