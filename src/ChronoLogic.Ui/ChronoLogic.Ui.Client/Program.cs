using ChronoLogic.Api.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var app = builder.Build();

await builder.Build().RunAsync();