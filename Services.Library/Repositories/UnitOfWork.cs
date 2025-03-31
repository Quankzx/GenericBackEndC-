using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Services.Library.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly ILogger<UnitOfWork> _logger;
    public UnitOfWork(DbContext context, ILogger<UnitOfWork> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var entries = _context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                _logger.LogInformation("Entity: {EntityName}, State: {State}", 
                    entry.Entity.GetType().Name, 
                    entry.State);
            }

            var result = await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("SaveChangesAsync result: {Result}", result);
            return result > 0;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error occurred while committing changes. Details: {Message}", ex.Message);
            throw;
        }
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("SaveChangesAsync result: {Result}", result);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while saving changes. Details: {Message}", ex.Message);
            throw;
        }
    }
    public void Rollback()
    {
        _logger.LogInformation("Rolling back changes");
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                case EntityState.Deleted:
                    entry.Reload();
                    break;
            }
        }
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}
