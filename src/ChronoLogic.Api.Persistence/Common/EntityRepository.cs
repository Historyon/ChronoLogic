using System.Linq.Expressions;
using ChronoLogic.Api.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChronoLogic.Api.Persistence.Common;

internal class EntityRepository<TEntity>(ChronoLogicDbContext context) : IEntityRepository<TEntity>
    where TEntity : Entity
{
    protected readonly ChronoLogicDbContext Context = context;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public async Task<TResult?> FindAsync<TResult>(Expression<Func<TEntity, TResult>> selector, 
        Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();
        
        if (predicate is not null) query = query.Where(predicate);
        
        return await query
            .AsNoTracking()
            .Select(selector)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult?> FindByIdAsync<TResult>(Guid id, Expression<Func<TEntity, TResult>> selector, 
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<TEntity>()
            .AsNoTracking()
            .Where(entity => entity.Id == id)
            .Select(selector)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TResult>> FindAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, 
        CancellationToken cancellationToken)
        => await FindAllAsync(selector, null, cancellationToken); 

    public async Task<IReadOnlyList<TResult>> FindAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, 
        Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();
        
        if (predicate is not null) query = query.Where(predicate);
        
        return await query
            .AsNoTracking()
            .Select(selector)
            .ToListAsync(cancellationToken);   
    }
    
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Set<TEntity>()
            .AsNoTracking()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}