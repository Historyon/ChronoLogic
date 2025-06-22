namespace ChronoLogic.Api.Persistence.Interfaces;

public interface IUserSession
{
    public Guid UserId { get; }
    void SetUserId(Guid userId);
}