using ChronoLogic.Api.Core.Dtos.Responses;
using ChronoLogic.Api.Persistence.Entities;

namespace ChronoLogic.Api.Core.Mappers;

internal static class UserMappers
{
    internal static UserLoginResponse MapToUserLoginResponse(this UserEntity userEntity)
    {
        return new UserLoginResponse
        {
            UserId = userEntity.Id,
            Username = userEntity.Username
        };
    }
}