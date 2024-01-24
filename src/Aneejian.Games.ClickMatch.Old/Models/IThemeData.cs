
namespace Aneejian.Games.ClickMatch.Models;

public interface IThemeData
{
	string? ThemeId { get; set; }
	string ThemeName { get; }
	string? ThemeType { get; set; }
	string? ThemeUrl { get; set; }
	int ItemCount { get; set; }
	IEnumerable<string>? ItemNames { get; set; }
	Dictionary<string, dynamic>? MoreInfo { get; set; }
	string Attribution { get; }
	bool AttributionRequired { get; }
	string Difficulty { get; }

	IEnumerable<string> GetItems(int itemsToReturn);
}