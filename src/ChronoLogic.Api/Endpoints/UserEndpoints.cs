using ChronoLogic.Api.Attributes;
using ChronoLogic.Api.Core.Dtos.Requests;
using ChronoLogic.Api.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace ChronoLogic.Api.Endpoints;

internal static class UserEndpoints
{
    internal static void MapUserEndpoints(this WebApplication app)
    {
        var userGroup = app.MapGroup("/users")
            .WithTags("Users");

        userGroup
            .MapPost("/", 
                async (CreateUserRequest createUserRequest, IUserService userService, 
                    CancellationToken cancellationToken) =>
                {
                    var user = await userService.CreateUserAsync(createUserRequest, cancellationToken);
                    return Results.Ok(user);
                })
            .WithMetadata(new RequireUserSessionAttribute())
            .WithOpenApi(o =>
            {
                o.Summary = "Create a new user";
                return o;
            });
        
        userGroup
            .MapGet("/", async (IUserService userService, CancellationToken cancellationToken) =>
            {
                var users = await userService.GetUsersAsync(cancellationToken);
                return Results.Ok(users);
            })
            .WithOpenApi(o =>
            {
                o.Summary = "Get all users";
                return o;
            });

        userGroup
            .MapDelete("/{userId:guid}",
                async (Guid userId, IUserService userService, CancellationToken cancellationToken) =>
                {
                    await userService.DeleteUserAsync(userId, cancellationToken);
                    return Results.NoContent();
                })
            .WithMetadata(new RequireUserSessionAttribute())
            .WithOpenApi(o =>
            {
                o.Summary = "Delete a user";
                return o;
            });
    }
}