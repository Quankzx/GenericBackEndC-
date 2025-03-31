
namespace Services.API.Configurations;
using Microsoft.OpenApi.Models;

public static class SwaggerGenConfiguration
{
    public static IServiceCollection AddSwaggerGenConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Cấu hình Swagger
        services.AddSwaggerGen(options =>
        {
            // Thêm thông tin về Bearer Authorization
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "Please enter the Bearer token (without 'Bearer' prefix)",
            });

            // Cấu hình yêu cầu bảo mật cho tất cả các API
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }
    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        // Cấu hình sử dụng Swagger trong pipeline
        app.UseSwagger();
        app.UseSwaggerUI();
        return app;
    }
}


