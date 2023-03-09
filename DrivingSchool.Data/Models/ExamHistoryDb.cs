namespace DrivingSchool.Data.Models;

public class ExamHistoryDb
{
    public int Id { get; init; }
    public int CorrectAnswers { get; init; }
    public int WrongAnswers { get; init; }
    public TimeSpan TotalTime { get; init; }
    public DateTime Date { get; init; }

    public int UserId { get; init; }
    public UserDb? User { get; init; }

    public int TicketId { get; init; }
    public ExamTicketDb Ticket { get; init; } = null!;
}