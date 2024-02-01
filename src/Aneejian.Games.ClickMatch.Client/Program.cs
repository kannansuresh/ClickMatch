using Aneejian.Games.ClickMatch;
using Aneejian.Games.ClickMatch.Client;
using Aneejian.Games.ClickMatch.Constants;
using Aneejian.Games.ClickMatch.Services;
using Aneejian.Games.ClickMatch.Services.Authentication;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<IThemeService>(sp => new ThemeService(new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }, AppStrings.ConfigFilePath));

builder.Services.AddSingleton<BaseAuthenticationService>();

builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AuthStateProvider>();

builder.Services.AddScoped<IndexedDbService>();

builder.Services.AddScoped<SessionStorageService>();

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
