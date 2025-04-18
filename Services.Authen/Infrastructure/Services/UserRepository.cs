﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Authen.Domain.Entities;
using Services.Authen.Infrastructure.Persistence;
using Services.Authen.Infrastructure.Repositories;
using Services.Library.Repositories;
using System.Threading.Tasks;

namespace Services.Authen.Infrastructure.Services;

public class UserRepository: Repository<User>,IUserRepository
{
    private readonly AuthDbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(
        AuthDbContext context, 
        IUnitOfWork unitOfWork,
        ILogger<UserRepository> logger) : base(context, unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        try
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == userName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting user by username: {Username}", userName);
            throw;
        }
    }

    public override async Task AddAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            var result = await _unitOfWork.CommitAsync();
            
            if (!result)
            {
                _logger.LogWarning("Failed to commit user registration for username: {Username}", user.Username);
                throw new Exception("Failed to save user to database");
            }
            
            _logger.LogInformation("Successfully registered user with username: {Username}", user.Username);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding user: {Username}", user.Username);
            throw;
        }
    }
    public async Task UpdateAsync(User user)
    {
        try
        {
            _context.Users.Update(user);
            var result = await _unitOfWork.CommitAsync();
            if (!result)
            {
                _logger.LogWarning("Failed to commit user registration for username: {Username}", user.Username);
                throw new Exception("Failed to save user to database");
            }

            _logger.LogInformation("Successfully registered user with username: {Username}", user.Username);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding user: {Username}", user.Username);
            throw;
        }
    }
    public async Task<User?> GetByRefreshTokenAsync(string refreshToken)
    {
        try
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.RefeshToken == refreshToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting user by username: {RefeshToken}", refreshToken);
            throw;
        }
    }
}
