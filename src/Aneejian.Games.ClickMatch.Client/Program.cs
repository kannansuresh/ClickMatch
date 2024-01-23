using Aneejian.Games.ClickMatch.Client;
using Aneejian.Games.ClickMatch.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IThemeService>(sp => new ThemeService(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }, "config/config.json"));

builder.Services.AddScoped<IndexedDbInterop>();

builder.Services.AddSingleton<GameManagerService>();

var host = builder.Build();

try
{
	var themeService = host.Services.GetRequiredService<IThemeService>();
	await themeService.InitializeAsync();
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
}



await host.RunAsync();
