using MediatR;
using Services.Authen.Application.DTOs;
namespace Services.Authen.Application.Commands;

public class RefreshTokenCommand : IRequest<AuthResponse>
{
    public string RefreshToken { get; set; } = string.Empty;
}
