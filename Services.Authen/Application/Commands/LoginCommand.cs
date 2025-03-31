using MediatR;
using Services.Authen.Application.DTOs;
namespace Services.Authen.Application.Commands;

public class LoginCommand : IRequest<AuthResponse>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
