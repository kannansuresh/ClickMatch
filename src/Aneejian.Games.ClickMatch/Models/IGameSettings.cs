
namespace Aneejian.Games.ClickMatch.Models
{
	public interface IGameSettings
	{
		IEnumerable<string> Items { get; }
		int NumberOfTiles { get; set; }
		bool ShowTileNumbers { get; set; }
		IThemeData ThemeData { get; set; }

		IEnumerable<TileModel> GenerateTiles();
	}
}