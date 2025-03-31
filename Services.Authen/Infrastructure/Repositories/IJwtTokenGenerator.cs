using Services.Authen.Domain.Entities;
namespace Services.Authen.Infrastructure.Repositories;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
