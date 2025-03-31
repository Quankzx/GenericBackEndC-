using FluentValidation;
using MediatR;
using Services.Authen.Application.Commands;
using Services.Authen.Application.DTOs;
using Services.Authen.Infrastructure.Repositories;

namespace Services.Authen.Application.Handlers;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwt;
    private readonly IValidator<LoginCommand> _validator;
    public LoginCommandHandler(IJwtTokenGenerator jwt, IValidator<LoginCommand> validator, IUserRepository userRepository)
    {
        _jwt = jwt;
        _userRepository = userRepository;
        _validator = validator;
    }
    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var user = await _userRepository.GetByUserNameAsync(request.Username);
        if (user == null )
        {
            throw new UnauthorizedAccessException("User not found");
        }
        if(!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid password");
        }
        var token = _jwt.GenerateAccessToken(user);
        var refreshToken = _jwt.GenerateRefreshToken();

        user.RefeshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userRepository.UpdateAsync(user);

        return new AuthResponse { Token = token,RefeshToken = refreshToken };
    }
}
