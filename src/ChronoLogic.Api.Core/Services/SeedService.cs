using ChronoLogic.Api.Persistence.Entities;
using ChronoLogic.Api.Persistence.Interfaces;
using ChronoLogic.Api.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using NodaTime;

namespace ChronoLogic.Api.Core.Services;

public interface ISeedService
{
    Task SeedDefaultUserAsync(CancellationToken cancellationToken = default);
}

internal class SeedService(IConfiguration configuration, IUserRepository userRepository, IClock clock, 
    IUserSession userSession) : ISeedService
{
    public async Task SeedDefaultUserAsync(CancellationToken cancellationToken = default)
    {
        var userIdString = configuration["DefaultUser:Id"];
        
        if (!Guid.TryParse(userIdString, out var defaultUserId)) 
            throw new InvalidOperationException("Default user ID is invalid.");
        
        var userExists = await userRepository.ExistsUserOrDeletedUserAsync(defaultUserId, cancellationToken);
        
        if (userExists) return;
        
        userSession.SetUserId(defaultUserId);
        await userRepository.AddAsync(new UserEntity
        {
            Id = defaultUserId,
            Username = configuration["DefaultUser:Username"] ?? "Admin",
            Roles = new List<UserRoleEntity>
            {
                new() { Role = RoleType.Admin }
            }
        }, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);
    }
}