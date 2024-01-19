using Aneejian.Games.ClickMatch.Models;

namespace Aneejian.Games.ClickMatch.Services
{
	public interface IThemeService
	{
		Config? ThemeConfig { get; }
		IEnumerable<IThemeData>? ThemeDatas { get; }
		bool IsInitialized { get; }
		Task InitializeAsync();
	}
}