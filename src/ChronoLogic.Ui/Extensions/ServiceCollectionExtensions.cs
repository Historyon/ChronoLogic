using ChronoLogic.Api.Client;
using ChronoLogic.Ui.Services;
using MudBlazor;
using MudBlazor.Services;

namespace ChronoLogic.Ui.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChronoLogicUiServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
            config.SnackbarConfiguration.PreventDuplicates = true;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 10000;
        });
        
        services.AddHttpClient<ChronoLogicApiClient>(client =>
        {
            var apiBaseUrl = configuration["ChronoLogicApi:BaseUrl"];
    
            if (string.IsNullOrWhiteSpace(apiBaseUrl))
                throw new InvalidOperationException("ChronoLogic API base URL is not set.");
    
            client.BaseAddress = new Uri(apiBaseUrl);
        });

        services.AddScoped<IUserSessionService, UserSessionService>();
        services.AddScoped<IUserApiService, UserApiService>();

        return services;
    }
}