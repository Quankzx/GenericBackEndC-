using Microsoft.EntityFrameworkCore;
using Services.Authen.Application.Handlers;
using Services.Authen.Infrastructure.Persistence;
using Services.Authen.Infrastructure.Repositories;
using Services.Authen.Infrastructure.Services;
using Services.Library.Repositories;
using FluentValidation;
using Services.Authen.Application.Validators;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionStringSql = builder.Configuration.GetConnectionString("SQLConnection");

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(connectionStringSql));


builder.Services.AddScoped<DbContext, AuthDbContext>(); 

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// 3. Đăng ký Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<LoginCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));



// 4. Đăng ký JwtTokenGenerator
builder.Services.AddSingleton<IJwtTokenGenerator>(new JwtTokenGenerator(builder.Configuration["Jwt:Key"]!));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
