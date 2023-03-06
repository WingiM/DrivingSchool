namespace DrivingSchool.Data.Models;

public class ExamTicketDb
{
    public int Id { get; init; }
    public int Number { get; init; }

    public List<ExamTicketQuestionDb> Questions { get; init; } = null!;
}