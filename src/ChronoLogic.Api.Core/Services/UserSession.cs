using ChronoLogic.Api.Persistence.Interfaces;

namespace ChronoLogic.Api.Core.Services;

internal class UserSession : IUserSession
{
    public Guid UserId { get; private set; }

    public void SetUserId(Guid userId) => UserId = userId;
}