using System.Linq.Expressions;
using ChronoLogic.Api.Persistence.Common;

namespace ChronoLogic.Api.Persistence.Interfaces;

public interface IEntityRepository<TEntity> where TEntity : Entity
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    Task<TResult?> FindAsync<TResult>(Expression<Func<TEntity, TResult>> selector, 
        Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default);
    Task<TResult?> FindByIdAsync<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector, 
        CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TResult>> FindAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, 
        CancellationToken cancellationToken);
    Task<IReadOnlyList<TResult>> FindAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}