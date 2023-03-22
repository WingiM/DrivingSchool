namespace DrivingSchool.Data.Models;

public class ExamTicketQuestionAnswerDb : BaseEntity
{
    public int NumberInQuestion { get; init; }
    public string AnswerText { get; init; } = null!;
    public bool IsCorrect { get; init; }
    public int QuestionId { get; init; }
    public ExamTicketQuestionDb? Question { get; init; }
}