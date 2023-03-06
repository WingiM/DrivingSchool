namespace DrivingSchool.Domain.Models;

public class ExamTicket
{
    public int Id { get; init; }
    public required int Number { get; init; }

    public required ExamTicketQuestion[] Questions { get; init; }
}