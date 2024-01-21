
namespace Aneejian.Games.ClickMatch.Models
{
	public interface IGameSettings
	{
		IEnumerable<string> Items { get; }
		int NumberOfTiles { get; }
		bool ShowTileNumbers { get; set; }
		IThemeData ThemeData { get; set; }

		void SetupNextLevel(int newTileCount = 0);
		IEnumerable<TileModel> GenerateTiles();
	}
}