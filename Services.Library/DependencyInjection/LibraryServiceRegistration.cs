using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Library.Db;
using Services.Library.Repositories;
namespace Services.Library.DependencyInjection;

public static class LibraryServiceRegistration
{
    public static IServiceCollection AddLibraryServices<TDbContext>(this IServiceCollection services, string connectionString) where TDbContext : DbContext
    {
        services.AddDbContext<TDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}
