using MediatR;
using Services.Authen.Application.Commands;
using Services.Authen.Application.DTOs;
using Services.Authen.Domain.Entities;
using Services.Authen.Infrastructure.Repositories;


namespace Services.Authen.Application.Handlers;
public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwt;
    public RegisterCommandHandler(IUserRepository userRepository , IJwtTokenGenerator jwt)
    {
        _userRepository = userRepository;
        _jwt = jwt;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User { Username = request.Username, PasswordHash = hashedPassword, Email = request.Email };
            await _userRepository.AddAsync(user);

            var token = _jwt.GenerateAccessToken(user);
            return new AuthResponse { Token = token };
        }catch(Exception ex)
        {
              return new AuthResponse { Token = "" };
        }
       
    }
}
