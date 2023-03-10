namespace DrivingSchool.Domain.Models;

public class ExamTicketQuestionAnswer
{
    public int Id { get; init; }
    public required int NumberInQuestion { get; init; }
    public required string AnswerText { get; init; }
    public bool IsCorrect { get; init; }
    public int QuestionId { get; init; }
}