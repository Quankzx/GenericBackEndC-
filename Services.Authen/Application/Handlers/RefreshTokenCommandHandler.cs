using FluentValidation;
using MediatR;
using Services.Authen.Application.Commands;
using Services.Authen.Application.DTOs;
using Services.Authen.Infrastructure.Repositories;

namespace Services.Authen.Application.Handlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwt;
        private readonly IValidator<RefreshTokenCommand> _validator;
        public RefreshTokenCommandHandler(IJwtTokenGenerator jwt, IValidator<RefreshTokenCommand> validator, IUserRepository userRepository)
        {
            _jwt = jwt;
            _userRepository = userRepository;
            _validator = validator;
        }
        public async Task<AuthResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
                var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);
                if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
                {
                    throw new ValidationException("Invalid or expired refresh token");
                }

                var newAccessToken = _jwt.GenerateAccessToken(user);
                var newRefreshToken = _jwt.GenerateRefreshToken();

                user.RefeshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _userRepository.UpdateAsync(user);

                return new AuthResponse { Token = newAccessToken, RefeshToken = newRefreshToken };
            }
            catch (Exception)
            {
                return new AuthResponse { Token = "" , RefeshToken =""};
            }
        }
    }
}
