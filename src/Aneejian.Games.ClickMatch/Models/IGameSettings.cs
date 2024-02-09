
namespace Aneejian.Games.ClickMatch.Models;

public interface IGameSettings
{
	IEnumerable<string> Items { get; }
	int NumberOfTiles { get; }
	bool ShowTileNumbers { get; set; }
	int GameLevel { get; }
	IThemeData ThemeData { get; set; }

	void SetupNextLevel(int newLevel = 0);
	IEnumerable<TileModel> GenerateTiles();
}