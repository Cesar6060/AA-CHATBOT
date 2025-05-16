namespace AAU_BE.Models;

public class LoginRequest
{
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; } = string.Empty;
}