using System.ComponentModel.DataAnnotations;

namespace ChronoLogic.Api.Persistence.Common;

public abstract class Entity : AuditableEntity
{
    [Key]
    public Guid Id { get; set; }
}