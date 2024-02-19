using Aneejian.Games.ClickMatch;
using Aneejian.Games.ClickMatch.Client;
using Aneejian.Games.ClickMatch.Helpers;
using Aneejian.Games.ClickMatch.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddClickMatch(builder.HostEnvironment.BaseAddress);

var host = builder.Build();

try
{
	var themeService = host.Services.GetRequiredService<IThemeService>();
	await themeService.InitializeAsync();
}
catch (Exception ex)
{
	SharedMethods.Log(ex.ToString());
}

await host.RunAsync();