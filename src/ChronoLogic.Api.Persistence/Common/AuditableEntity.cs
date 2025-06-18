using System.ComponentModel.DataAnnotations;
using NodaTime;

namespace ChronoLogic.Api.Persistence.Common;

public abstract class AuditableEntity
{
    [Required]
    public Instant CreatedAt { get; set; }
    [Required]
    public Guid CreatedBy { get; set; } = Guid.Empty;
    [Required]
    public Instant? UpdatedAt { get; set; }
    [Required]
    public Guid? UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }
    public Instant? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
}