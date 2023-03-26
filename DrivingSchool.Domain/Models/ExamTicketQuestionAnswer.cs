namespace DrivingSchool.Domain.Models;

public class ExamTicketQuestionAnswer : Entity
{
    public required int NumberInQuestion { get; init; }
    public required string AnswerText { get; init; }
    public bool IsCorrect { get; init; }
    public int QuestionId { get; init; }
}