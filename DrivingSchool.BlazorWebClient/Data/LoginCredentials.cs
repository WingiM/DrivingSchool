namespace DrivingSchool.BlazorWebClient.Data;

public class LoginCredentials
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}