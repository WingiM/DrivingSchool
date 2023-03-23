namespace DrivingSchool.Data.Models;

public class ExamTicketDb : Entity
{
    public int Number { get; init; }

    public List<ExamTicketQuestionDb> Questions { get; init; } = null!;
}