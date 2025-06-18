using System.ComponentModel.DataAnnotations;
using ChronoLogic.Api.Persistence.Common;

namespace ChronoLogic.Api.Persistence.Entities;

public class UserEntity : Entity
{
    [Required, StringLength(100, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;
}