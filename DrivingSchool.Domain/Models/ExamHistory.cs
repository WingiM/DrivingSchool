namespace DrivingSchool.Domain.Models;

public class ExamHistory
{
    public int Id { get; init; }
    public required int CorrectAnswers { get; init; }
    public required int WrongAnswers { get; init; }
    public required TimeSpan TotalTime { get; init; }
    public required DateTime Date { get; init; }
    
    public required int UserId { get; init; }
    public UserInitials? User { get; init; }
    public required int TicketId { get; init; }
    public int? TicketNumber { get; init; }
}