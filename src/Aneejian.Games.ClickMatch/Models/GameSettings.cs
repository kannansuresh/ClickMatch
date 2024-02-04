namespace Aneejian.Games.ClickMatch.Models;

public class GameSettings(int gameLevel, IThemeData themeData, bool showTileNumber = true) : IGameSettings
{
	public int GameLevel { get; private set; } = gameLevel;

	public int NumberOfTiles => GameLevel * 4;

	public IThemeData ThemeData { get; set; } = themeData;

	public bool ShowTileNumbers { get; set; } = showTileNumber;

	public IEnumerable<string> Items => ThemeData.GetItems(NumberOfTiles / 2) ?? [];

	public void SetupNextLevel(int newLevel = 0)
	{
		GameLevel = newLevel == 0 ? GameLevel + 1 : newLevel;
	}

	public IEnumerable<TileModel> GenerateTiles()
	{
		var items = Items.ToArray();

		List<TileModel> tiles = [];

		var uniquePositions = new HashSet<int>(NumberOfTiles);

		while (uniquePositions.Count < NumberOfTiles)
		{
			uniquePositions.Add(Random.Shared.Next(1, NumberOfTiles + 1));
		}

		var itemIndex = 0;
		var positionIndex = 0;

		while (tiles.Count < NumberOfTiles)
		{
			var tilePairId = Guid.NewGuid();
			var content = items[itemIndex];
			var tile1PositionId = uniquePositions.ElementAt(positionIndex);
			var tile2PositionId = uniquePositions.ElementAt(positionIndex + 1);
			var tile1 = new TileModel(tilePairId, tile1PositionId, content);
			var tile2 = new TileModel(tilePairId, tile2PositionId, content);
			tiles.Add(tile1);
			tiles.Add(tile2);
			itemIndex++;
			positionIndex += 2;
		}

		return tiles.OrderBy(x => x.PositionId);
	}
}
