using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChronoLogic.Api.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ChronoLogicDbContext>(options => options
            .UseNpgsql(config.GetConnectionString("ChronoLogicDb"), m =>
            {
                m.MigrationsAssembly(typeof(ChronoLogicDbContext).Assembly.GetName().Name);
                m.UseNodaTime();
            })
            .UseSnakeCaseNamingConvention() );
        
        return services;
    }
}