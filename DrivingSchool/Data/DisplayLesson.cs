namespace DrivingSchool.Data;

public class DisplayLesson
{
    public int Id { get; set; }
    public DateTime TimeStart { get; set; }
    public DateTime TimeEnd { get; set; }
    public string Text { get; set; } = string.Empty;
}