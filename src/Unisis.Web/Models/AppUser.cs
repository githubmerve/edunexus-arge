namespace Unisis.Web.Models;

public class AppUser
{
    public string StudentNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "Student";
    public string? FirebaseEmail { get; set; }
    public string? FirebaseIdToken { get; set; }
}
