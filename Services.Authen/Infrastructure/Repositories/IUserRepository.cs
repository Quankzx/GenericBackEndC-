using Services.Authen.Domain.Entities;
using Services.Library.Repositories;

namespace Services.Authen.Infrastructure.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName);
    Task<User?> GetByRefreshTokenAsync(string refreshToken);
    Task UpdateAsync(User user);


}
