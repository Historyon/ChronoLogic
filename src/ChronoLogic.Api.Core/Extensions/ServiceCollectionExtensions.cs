using ChronoLogic.Api.Core.Services;
using ChronoLogic.Api.Persistence.Extensions;
using ChronoLogic.Api.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace ChronoLogic.Api.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration config)
    {
        services.AddPersistence(config);
        services.AddScoped<IUserSession, UserSession>();
        services.AddSingleton<IClock>(SystemClock.Instance);

        return services;
    }
}