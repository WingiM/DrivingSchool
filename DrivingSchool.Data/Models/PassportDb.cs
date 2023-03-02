namespace DrivingSchool.Data.Models;

public class PassportDb
{
    public int Id { get; init; }
    public string Series { get; init; } = null!;
    public string Number { get; init; } = null!;
    public DateTime IssueDate { get; init; }
    public string IssuedBy { get; init; } = null!;
    public string IssuerCode { get; init; } = null!;
    public string PlaceOfBirth { get; init; } = null!;

    public int UserId { get; init; }
    public UserDb? User { get; init; }
}