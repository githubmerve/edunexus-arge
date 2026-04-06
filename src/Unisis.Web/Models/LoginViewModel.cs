namespace Unisis.Web.Models;

public class LoginViewModel
{
    public string UserCode { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserType { get; set; } = "Ogrenci";
    public string? ErrorMessage { get; set; }
}
