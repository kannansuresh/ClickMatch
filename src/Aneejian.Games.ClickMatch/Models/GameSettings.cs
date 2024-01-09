namespace Aneejian.Games.ClickMatch.Models;

public class GameSettings(int numberOfTiles, IThemeData themeData)
{
	public int NumberOfTiles { get; set; } = numberOfTiles % 2 == 0 ? numberOfTiles : numberOfTiles + 1;
	public IThemeData ThemeData { get; set; } = themeData;
	public bool ShowTileNumbers { get; set; } = false;
	public IEnumerable<string> Items => ThemeData.GetItems(NumberOfTiles / 2) ?? [];

	public IEnumerable<TileModel> GenerateTiles()
	{
		var items = Items.ToArray();

		List<TileModel> tiles = [];

		var uniquePositions = new HashSet<int>(NumberOfTiles);


		while (uniquePositions.Count < NumberOfTiles)
		{
			uniquePositions.Add(Random.Shared.Next(1, NumberOfTiles + 1));
		}

		Console.WriteLine(string.Join(",", uniquePositions));

		var itemIndex = 0;
		var positionIndex = 0;

		while (tiles.Count < NumberOfTiles)
		{
			var tilePairId = Guid.NewGuid();
			var content = items[itemIndex];
			var tile1PositionId = uniquePositions.ElementAt(positionIndex);
			var tile2PositionId = uniquePositions.ElementAt(positionIndex + 1);
			var tile1 = new TileModel(tilePairId, tile1PositionId, content, ShowTileNumbers);
			var tile2 = new TileModel(tilePairId, tile2PositionId, content, ShowTileNumbers);
			tiles.Add(tile1);
			tiles.Add(tile2);
			itemIndex++;
			positionIndex += 2;
		}

		return tiles.OrderBy(x => x.PositionId);
	}
}
