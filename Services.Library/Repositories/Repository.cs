using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
namespace Services.Library.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DbContext _context;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DbSet<TEntity> dbSet;
    public Repository(DbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
        dbSet = context.Set<TEntity>();
    }
    public virtual Task<List<TEntity>> FindAllAsync() => dbSet.ToListAsync();
    public virtual Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken) => dbSet.ToListAsync(cancellationToken);

    public virtual Task AddAsync(TEntity entity) => dbSet.AddAsync(entity).AsTask();
    public virtual Task AddAsync(TEntity entity, CancellationToken cancellationToken) => dbSet.AddAsync(entity, cancellationToken).AsTask();

    public virtual Task AddRangeAsync(IEnumerable<TEntity> entities) => dbSet.AddRangeAsync(entities);
    public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken) => dbSet.AddRangeAsync(entities, cancellationToken);

    public void Update(TEntity entity) => dbSet.Update(entity);
    public void Update(IEnumerable<TEntity> entities) => dbSet.UpdateRange(entities);

    public virtual Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate) => dbSet.AnyAsync(predicate);
    public virtual Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) => dbSet.AnyAsync(predicate, cancellationToken);

    public virtual Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate) => dbSet.Where(predicate).ToListAsync();
    public virtual Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) => dbSet.Where(predicate).ToListAsync(cancellationToken);

    public virtual Task<TEntity?> FindUniqueAsync(Expression<Func<TEntity, bool>> predicate) => dbSet.FirstOrDefaultAsync(predicate);
    public virtual Task<TEntity?> FindUniqueAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken) => dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

    public void Remove(TEntity entity) => dbSet.Remove(entity);
    public void RemoveRange(IEnumerable<TEntity> entities) => dbSet.RemoveRange(entities);
    public async Task<bool> SaveChangesAsync() => await _unitOfWork.CommitAsync();
}
