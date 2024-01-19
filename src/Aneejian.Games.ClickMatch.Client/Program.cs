using Aneejian.Games.ClickMatch.Client;
using Aneejian.Games.ClickMatch.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IThemeService>(sp => new ThemeService(sp.GetRequiredService<HttpClient>(), "config/config.json"));

builder.Services.AddSingleton<GameManagerService>();

var host = builder.Build();

var themeService = host.Services.GetRequiredService<IThemeService>();
await themeService.InitializeAsync();

await host.RunAsync();
