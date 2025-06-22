using ChronoLogic.Api.Persistence.Common;
using ChronoLogic.Api.Persistence.Entities;
using ChronoLogic.Api.Persistence.Exceptions;
using ChronoLogic.Api.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChronoLogic.Api.Persistence.Repositories;

public interface IUserRepository : IEntityRepository<UserEntity>
{
    Task<UserEntity?> FindWithRolesByIdAsync(Guid userId, CancellationToken cancellationToken);
    void DeleteUser(UserEntity userEntity);
    Task<bool> ExistsUserOrDeletedUserAsync(Guid userId, CancellationToken cancellationToken);
}

internal class UserRepository(ChronoLogicDbContext context) : EntityRepository<UserEntity>(context), IUserRepository
{
    public Task<UserEntity?> FindWithRolesByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return Context.Users
            .Include(user => user.Roles)
            .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);
    }

    public void DeleteUser(UserEntity userEntity)
    {
        Context.Users.Remove(userEntity);

        foreach (var role in userEntity.Roles)
            Context.Roles.Remove(role);
    }

    public Task<bool> ExistsUserOrDeletedUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        return Context.Users
            .IgnoreQueryFilters()
            .AnyAsync(user => user.Id == userId, cancellationToken);
    }
}