namespace DrivingSchool.Data;

public class EditPassport
{
    public int Id { get; set; }
    public string Series { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public DateTime? IssueDate { get; set; }
    public string IssuedBy { get; set; } = string.Empty;
    public string IssuerCode { get; set; } = string.Empty;
    public string PlaceOfBirth { get; set; } = string.Empty;
}