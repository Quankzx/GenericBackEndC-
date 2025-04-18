﻿using System.Linq.Expressions;
namespace Services.Library.Repositories;

public interface IRepository<TEntity>
{
    Task<List<TEntity>> FindAllAsync();
    Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entity);
    void Update(IEnumerable<TEntity> entities);

    Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<List<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity?> FindUniqueAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<TEntity?> FindUniqueAsync(Expression<Func<TEntity, bool>> predicate);

    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate);

    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
