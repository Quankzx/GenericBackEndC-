using Services.Authen.Domain.Entities;
namespace Services.Authen.Infrastructure.Repositories;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
