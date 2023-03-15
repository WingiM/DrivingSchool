namespace DrivingSchool.Data.Models;

public class ExamTicketQuestionDb : BaseEntity
{
    public string Question { get; init; } = null!;
    public string? ImageSource { get; init; }
    public string Comment { get; init; } = null!;
    public int NumberInTicket { get; init; }
    public int TicketId { get; init; }
    public ExamTicketDb? Ticket { get; init; }

    public List<ExamTicketQuestionAnswerDb> Answers { get; init; } = null!;
}