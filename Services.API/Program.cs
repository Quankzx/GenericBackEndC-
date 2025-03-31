using Microsoft.AspNetCore.Authentication;
using Services.API.Configurations;
using Services.API.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddSwaggerGenConfiguration(builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();

app.UseCustomMiddleware();

app.MapControllers();

app.Run();
