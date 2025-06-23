using ChronoLogic.Api.Core.Dtos.Requests;
using ChronoLogic.Api.Core.Dtos.Responses;
using ChronoLogic.Api.Core.Mappers;
using ChronoLogic.Api.Persistence.Entities;
using ChronoLogic.Api.Persistence.Exceptions;
using ChronoLogic.Api.Persistence.Repositories;

namespace ChronoLogic.Api.Core.Services;

public interface IUserService
{
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserLoginResponse> CreateUserAsync(CreateUserRequest createUserRequest, CancellationToken cancellationToken);
    Task<UserLoginResponse> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<IReadOnlyList<UserLoginResponse>> GetUsersAsync(CancellationToken cancellationToken);
}

internal class UserService(IUserRepository userRepository) : IUserService
{
    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindWithRolesByIdAsync(userId, cancellationToken);

        if (user is null) throw new EntityNotFoundException(typeof(UserEntity), userId.ToString());
        
        if (user.Roles.Any(role => role.Role == RoleType.Admin))
            throw new AdminUserCannotBeDeletedException(userId.ToString());
        
        userRepository.DeleteUser(user);
        await userRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserLoginResponse> CreateUserAsync(CreateUserRequest createUserRequest, 
        CancellationToken cancellationToken)
    {
        var user = new UserEntity
        {
            Username = createUserRequest.Username,
            Roles = new List<UserRoleEntity>
            {
                new() { Role = RoleType.StandardUser }
            }
        };
        
        await userRepository.AddAsync(user, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return user.MapToUserLoginResponse();
    }

    public async Task<UserLoginResponse> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindWithRolesByIdAsync(userId, cancellationToken);
        
        if (user is null) throw new EntityNotFoundException(typeof(UserEntity), userId.ToString());
        
        return user.MapToUserLoginResponse();
    }

    public async Task<IReadOnlyList<UserLoginResponse>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return await userRepository
            .FindAllAsync(user => user.MapToUserLoginResponse(), cancellationToken);
    }
}