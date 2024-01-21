namespace Aneejian.Games.ClickMatch.Models;

public class GameSettings(int numberOfTiles, IThemeData themeData, bool showTileNumber = true) : IGameSettings
{
	public int NumberOfTiles { get; private set; } = GetNearestMultipleFor4(numberOfTiles);
	public IThemeData ThemeData { get; set; } = themeData;
	public bool ShowTileNumbers { get; set; } = showTileNumber;
	public IEnumerable<string> Items => ThemeData.GetItems(NumberOfTiles / 2) ?? [];

	public int GameLevel => NumberOfTiles / 4;

	public void SetupNextLevel(int newTileCount = 0)
	{
		if (newTileCount > 0)
			NumberOfTiles = GetNearestMultipleFor4(newTileCount);
		else
			NumberOfTiles += 4;
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

	private static int GetNearestMultipleFor4(int number)
	{
		int remainder = number % 4;
		return remainder == 0 ? number : number + (4 - remainder);
	}
}
