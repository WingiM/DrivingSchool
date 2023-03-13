using DrivingSchool.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DrivingSchool.Data.Configurations;

public class StudentLessonEntityConfiguration : IEntityTypeConfiguration<StudentLessonDb>
{
    public void Configure(EntityTypeBuilder<StudentLessonDb> builder)
    {
        builder.ToTable("student_lesson", "public");
        builder.HasKey(x => x.Id).HasName("student_lesson_pkey");

        builder.Property(x => x.Id).HasColumnName("id");
        builder.Property(x => x.StudentId).HasColumnName("student_id");
        builder.Property(x => x.TeacherId).HasColumnName("teacher_id");
        builder.Property(x => x.Date).HasColumnName("date");
        builder.Property(x => x.TimeStart).HasColumnName("time_start");
        builder.Property(x => x.DurationInMinutes).HasColumnName("duration");
    }
}