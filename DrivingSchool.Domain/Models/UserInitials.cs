namespace DrivingSchool.Domain.Models;

public class UserInitials
{
    public int Id { get; init; }
    public required string Surname { get; init; }
    public required string Name { get; init; }
    public required string Patronymic { get; init; }

    public override string ToString()
    {
        return $"{Surname} {Name} {Patronymic}";
    }
}