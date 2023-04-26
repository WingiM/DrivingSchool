namespace DrivingSchool.BlazorWebClient.Data;

public class EditUser
{
    public string Surname { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Patronymic { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailUpdated { get; set; }
    public DateTime? BirthDate { get; set; }
}