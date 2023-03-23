namespace DrivingSchool.Domain.Models;

public class ExamTicketQuestion : Entity
{
    public required string Question { get; init; }
    public string? ImageSource { get; init; }
    public required string Comment { get; init; }
    public required int NumberInTicket { get; init; }

    public required ExamTicketQuestionAnswer[] Answers { get; init; }
}