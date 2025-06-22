namespace ChronoLogic.Api.Persistence.Exceptions;

public class EntityNotFoundException : Exception
{
    public string EntityId { get; }
    public string EntityType { get; }

    public EntityNotFoundException(Type entityType, string entityId)
        : base($"The specified entity of type '{entityType.Name}' with ID '{entityId}' was not found.")
    {
        EntityType = entityType.Name;
        EntityId = entityId;
    }

    public EntityNotFoundException(Type entityType, string entityId, Exception innerException)
        : base($"The specified entity of type '{entityType.Name}' with ID '{entityId}' was not found.", innerException)
    {
        EntityType = entityType.Name;
        EntityId = entityId;
    }
}