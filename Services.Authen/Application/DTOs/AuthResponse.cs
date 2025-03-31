namespace Services.Authen.Application.DTOs;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string RefeshToken { get; set; } = string.Empty;

}
