using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class AvailableLessonEntityConfiguration : IEntityTypeConfiguration<AvailableLessonDb>
{
    public void Configure(EntityTypeBuilder<AvailableLessonDb> builder)
    {
        builder.ToTable("available_lesson", "public");
        builder.HasKey(x => x.Id).HasName("available_lesson_pkey");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.StudentId).HasColumnName("student_id");
        builder.Property(x => x.TeacherId).HasColumnName("teacher_id");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.TimeStart).HasColumnName("time_start");
        builder.Property(x => x.DurationInMinutes).HasColumnName("duration");
        builder.Property(x => x.IsTaken).HasColumnName("is_taken");

        builder
            .HasOne(x => x.Student)
            .WithMany(x => x.AvailableLessonsStudent)
            .HasForeignKey(x => x.StudentId);

        builder
            .HasOne(x => x.Teacher)
            .WithMany(x => x.AvailableLessons)
            .HasForeignKey(x => x.TeacherId);
    }
}