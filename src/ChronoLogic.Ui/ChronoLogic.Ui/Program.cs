using ChronoLogic.Api.Client;
using ChronoLogic.Ui.Components;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddMudServices();

builder.Services.AddHttpClient<ChronoLogicApiClient>(client =>
{
    var apiBaseUrl = builder.Configuration["ChronoLogicApi:BaseUrl"];
    
    if (string.IsNullOrWhiteSpace(apiBaseUrl))
        throw new InvalidOperationException("ChronoLogic API base URL is not set.");
    
    client.BaseAddress = new Uri(apiBaseUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ChronoLogic.Ui.Client._Imports).Assembly);

using (var scope = app.Services.CreateScope())
{
    var client = scope.ServiceProvider.GetRequiredService<ChronoLogicApiClient>();
    var users = await client.GetUsersAllAsync(CancellationToken.None);
}

app.Run();