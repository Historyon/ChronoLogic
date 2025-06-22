using ChronoLogic.Api.Attributes;
using ChronoLogic.Api.Persistence;
using ChronoLogic.Api.Persistence.Entities;
using ChronoLogic.Api.Persistence.Interfaces;
using ChronoLogic.Api.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChronoLogic.Api.Middlewares;

public class UserSessionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IEntityRepository<UserEntity> userRepository, 
        IUserSession userSession)
    {
        var endpoint = context.GetEndpoint();
        var requireUserSession = endpoint?.Metadata.GetMetadata<RequireUserSessionAttribute>();

        if (requireUserSession == null)
        {
            await next(context);
            return;
        }
        
        if (!context.Request.Headers.TryGetValue("X-User-Id", out var userIdString) ||
            !Guid.TryParse(userIdString, out var userId) ||
            !await userRepository.ExistsAsync(userId, context.RequestAborted))
        {
            if (requireUserSession.AbortOnFailure)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized. X-User-Id is missing or invalid.");
                return;
            }
        }
        else
        {
            userSession.SetUserId(userId);
        }
        
        await next(context);
    }
}