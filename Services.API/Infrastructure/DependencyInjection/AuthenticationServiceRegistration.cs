using FluentValidation;
using Services.Authen.Application.Handlers;
using Services.Authen.Application.Validators;
using Services.Authen.Infrastructure.Persistence;
using Services.Authen.Infrastructure.Repositories;
using Services.Authen.Infrastructure.Services;
using Services.Library.DependencyInjection;

namespace Services.API.Infrastructure.DependencyInjection;

public static class AuthenticationServiceRegistration
{
    public static IServiceCollection AddAuthenticationServices(
     this IServiceCollection services,
     IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SQLConnection");

        // LibraryServices sẽ đăng ký DbContext, IUnitOfWork và IRepository<T>
        services.AddLibraryServices<AuthDbContext>(connectionString);

        // Đăng ký Repository cụ thể
        services.AddScoped<IUserRepository, UserRepository>();

        // Đăng ký JwtTokenGenerator với lifetime Scoped (chọn một lifetime duy nhất)
        services.AddScoped<IJwtTokenGenerator>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var key = config["Jwt:Key"];
            return new JwtTokenGenerator(key);
        });
        // Đăng ký các validators từ assembly chứa LoginCommandValidator và RegisterCommandValidator
        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();

        // Đăng ký MediatR, gom các assembly liên quan thành một lần gọi
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(RegisterCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly);
        });

        return services;
    }
}
