using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using Services.API.Middlewares;
using Services.Authen.Application.Handlers;
using Services.Authen.Application.Validators;
using Services.Authen.Infrastructure.Persistence;
using Services.Authen.Infrastructure.Repositories;
using Services.Authen.Infrastructure.Services;
using Services.Library.DependencyInjection;
using System.Text;

namespace Services.API.Infrastructure.DependencyInjection;

public static class AuthenticationServiceRegistration
{
    public static IServiceCollection AddAuthenticationServices(
     this IServiceCollection services,
     IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SQLConnection");

        // Đăng ký JwtTokenGenerator với lifetime Scoped (chọn một lifetime duy nhất)
        services.AddSingleton<IJwtTokenGenerator>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var key = config["Jwt:Key"];
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            return new JwtTokenGenerator(key!, issuer!, audience!);
        });
        services.AddAuthentication("Bearer")
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
        });

        services.AddLibraryServices<AuthDbContext>(connectionString!);
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(RegisterCommandHandler).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly);
        });

        return services;
    }
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<AuthenticationMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}
