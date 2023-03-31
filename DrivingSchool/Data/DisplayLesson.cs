namespace DrivingSchool.Data;

public class DisplayLesson
{
    public int Id { get; init; }
    public DateTime TimeStart { get; init; }
    public DateTime TimeEnd { get; init; }
    public string Text { get; init; } = string.Empty;
}