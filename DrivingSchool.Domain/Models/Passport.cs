namespace DrivingSchool.Domain.Models;

public class Passport
{
    public int Id { get; init; }
    public required string Series { get; init; }
    public required string Number { get; init; }
    public required DateTime IssueDate { get; init; }
    public required string IssuedBy { get; init; }
    public required string IssuerCode { get; init; }
    public required string PlaceOfBirth { get; init; }
    public required int UserId { get; init; }
}