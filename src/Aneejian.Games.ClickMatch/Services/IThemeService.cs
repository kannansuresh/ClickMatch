using Aneejian.Games.ClickMatch.Models;

namespace Aneejian.Games.ClickMatch.Services
{
	public interface IThemeService
	{
		Task<Config> GetConfigAsync(string configPath);
		Task<IEnumerable<IThemeData>> GetThemesAsync(string localThemeInfo, string hostedThemeInfo);
	}
}