namespace DrivingSchool.Data;

public class CreateLesson
{
    public int TeacherId { get; set; }
    public int? StudentId { get; set; }
    public DateTime? Date { get; set; }
    public TimeSpan? TimeStart { get; set; }
    public int? Duration { get; set; }
}