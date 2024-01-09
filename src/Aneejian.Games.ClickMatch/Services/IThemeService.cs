
namespace Aneejian.Games.ClickMatch.Services;

public interface IThemeService
{
	Task<T?> GetAsync<T>(string path, bool deserialize);
}