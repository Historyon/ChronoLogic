namespace ChronoLogic.Api.Persistence.Exceptions;

[Serializable]
public class AdminUserCannotBeDeletedException : Exception
{
    public string UserId { get; }

    public AdminUserCannotBeDeletedException(string userId) 
        : base($"Admin user with ID '{userId}' cannot be deleted.")
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
        
        UserId = userId;
    }

    public AdminUserCannotBeDeletedException(string userId, Exception innerException) 
        : base($"Admin user with ID '{userId}' cannot be deleted.", innerException)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
        
        UserId = userId;
    }
}