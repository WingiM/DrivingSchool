namespace DrivingSchool.Domain.Models;

public class ExamTicket : Entity
{
    public required int Number { get; init; }

    public required ExamTicketQuestion[] Questions { get; init; }
}