using MediatR;
using Services.Authen.Application.DTOs;

namespace Services.Authen.Application.Commands;

public class RegisterCommand : IRequest<AuthResponse>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

}
