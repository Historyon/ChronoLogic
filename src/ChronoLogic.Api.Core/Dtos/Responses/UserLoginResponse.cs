namespace ChronoLogic.Api.Core.Dtos.Responses;

public class UserLoginResponse
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
}