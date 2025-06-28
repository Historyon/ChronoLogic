using ChronoLogic.Api.Core.Extensions;
using ChronoLogic.Api.Core.Services;
using ChronoLogic.Api.Endpoints;
using ChronoLogic.Api.Middlewares;
using NSwag;
using NSwag.Generation.Processors.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services
    .AddOpenApi()
    .AddCore(builder.Configuration)
    .AddOpenApiDocument(config =>
    {
        config.Title = "ChronoLogic API";
        config.Version = "v1";
        config.Description = "OpenAPI description for ChronoLogic API";
        
        config.OperationProcessors.Add(new OperationSecurityScopeProcessor("X-User-Id-Header"));
        config.AddSecurity("X-User-Id-Header", new OpenApiSecurityScheme
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "X-User-Id",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Set the user ID for API authorization."
        });
    });

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowOrigins", policyBuilder =>
        {
            policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
    app.UseCors("AllowOrigins");
}

app.UseHttpsRedirection();
app.UseMiddleware<UserSessionMiddleware>();

app.MapUserEndpoints();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ISeedService>();
    await seeder.SeedDefaultUserAsync();
}

app.Run();