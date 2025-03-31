using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Authen.Application.Commands;
using Services.Authen.Infrastructure.Persistence;
using Services.Authen.Infrastructure.Repositories;
using Services.Authen.Infrastructure.Services;
using Services.Library.DependencyInjection;

namespace Services.Authen.Infrastructure.DependencyInjection;

public static class AuthenticationServiceRegistration
{
    public static IServiceCollection AddAuthenticationServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        // Add Library Services with AuthDbContext
        services.AddLibraryServices<AuthDbContext>(connectionString);

        // Add Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Add Services
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        // Add MediatR
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly);
        });

        return services;
    }
} 