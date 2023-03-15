namespace DrivingSchool.Data.Models;

public class ExamTicketDb : BaseEntity
{
    public int Number { get; init; }

    public List<ExamTicketQuestionDb> Questions { get; init; } = null!;
}