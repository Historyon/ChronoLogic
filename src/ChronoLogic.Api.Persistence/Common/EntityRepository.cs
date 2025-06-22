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
        Expression<Func<TEntity, bool>>? predicate, bool asNoTracking, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();
        
        if (asNoTracking) query = query.AsNoTracking();
        if (predicate is not null) query = query.Where(predicate);
        
        return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TResult>> FindAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, 
        bool asNoTracking, CancellationToken cancellationToken)
        => await FindAllAsync(selector, null, asNoTracking, cancellationToken); 

    public async Task<IReadOnlyList<TResult>> FindAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>>? predicate, CancellationToken cancellationToken)
        => await FindAllAsync(selector, predicate, false, cancellationToken); 

    public async Task<IReadOnlyList<TResult>> FindAllAsync<TResult>(Expression<Func<TEntity, TResult>> selector, 
        Expression<Func<TEntity, bool>>? predicate, bool asNoTracking, CancellationToken cancellationToken)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();
        
        if (asNoTracking) query = query.AsNoTracking();
        if (predicate is not null) query = query.Where(predicate);
        
        return await query.Select(selector).ToListAsync(cancellationToken);   
    }
    
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await Context.Set<TEntity>()
            .AsNoTracking()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}