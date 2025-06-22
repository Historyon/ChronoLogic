using System.Linq.Expressions;
using ChronoLogic.Api.Persistence.Common;
using ChronoLogic.Api.Persistence.Entities;
using ChronoLogic.Api.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace ChronoLogic.Api.Persistence;

public class ChronoLogicDbContext(DbContextOptions<ChronoLogicDbContext> options, IClock clock, 
    IUserSession userSession) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleEntity> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(AuditableEntity).IsAssignableFrom(entity.ClrType))
            {
                var parameter = Expression.Parameter(entity.ClrType, "e");
                var filter = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, nameof(AuditableEntity.IsDeleted)),
                        Expression.Constant(false)
                    ),
                    parameter
                );
                
                entity.SetQueryFilter(filter);
            }
        }
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, 
        CancellationToken cancellationToken = new CancellationToken())
    {
        var now = clock.GetCurrentInstant();
        var userId = userSession.UserId;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.CreatedBy = userId;
                entry.Entity.UpdatedAt = now;
                entry.Entity.UpdatedBy = userId;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
                entry.Entity.UpdatedBy = userId;
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = now;
                entry.Entity.DeletedBy = userId;
            }
        }
        
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}