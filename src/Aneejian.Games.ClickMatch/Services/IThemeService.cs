using Aneejian.Games.ClickMatch.Models;

namespace Aneejian.Games.ClickMatch.Services
{
	public interface IThemeService
	{
		Task<Config> GetConfigAsync(string configPath);
		Task<List<ThemeData>> GetThemesAsync(string localThemeInfo, string hostedThemeInfo);
	}
}