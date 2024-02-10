using Aneejian.Games.ClickMatch.Constants;
using Aneejian.Games.ClickMatch.Services;
using Aneejian.Games.ClickMatch.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Aneejian.Games.ClickMatch;

public static class DependencyInjection
{
	public static IServiceCollection AddClickMatch(this IServiceCollection services, string baseAddress)
	{
		services.AddSingleton<IThemeService>(sp => new ThemeService(new HttpClient { BaseAddress = new Uri(baseAddress) }, AppStrings.ConfigFilePath));
		services.AddSingleton<AuthenticationService, AuthenticationService>();
		services.AddSingleton<IndexedDbService, IndexedDbService>();
		services.AddSingleton<SessionStorageService, SessionStorageService>();
		services.AddSingleton<GameManagerService, GameManagerService>();
		return services;
	}
}