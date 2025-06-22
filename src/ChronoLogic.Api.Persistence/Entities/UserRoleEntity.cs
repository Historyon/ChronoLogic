using ChronoLogic.Api.Persistence.Common;

namespace ChronoLogic.Api.Persistence.Entities;

public class UserRoleEntity : Entity
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    public RoleType Role { get; set; }
}

public enum RoleType
{
    Admin,
    StandardUser
}
