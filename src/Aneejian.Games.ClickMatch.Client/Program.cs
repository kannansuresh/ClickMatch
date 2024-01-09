using Aneejian.Games.ClickMatch.Client;
using Aneejian.Games.ClickMatch.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IThemeService>(sp => new ThemeService(sp.GetRequiredService<HttpClient>()));

await builder.Build().RunAsync();
