using Aneejian.Games.ClickMatch.Models;

namespace Aneejian.Games.ClickMatch.Services;

public class GameManagerService
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public IGameSettings GameSettings { get; set; } = null!;
	public TileModel[] Tiles { get; set; } = [];

	public int Moves { get; set; }
	public int Misses { get; set; }
	public bool GameInProgress { get; set; }
	public bool GameWon { get; set; }

	private List<TileModel>? FlippedTiles { get; set; } = [];
	private List<TileModel>? MatchedTiles { get; set; } = [];

	public void StartGame(IGameSettings gameSettings)
	{
		GameSettings = gameSettings ?? throw new Exception("Game settings not set.");
		FlippedTiles = [];
		MatchedTiles = [];
		Moves = 0;
		Misses = 0;
		GameWon = false;
		GameInProgress = true;
		Tiles = GameSettings.GenerateTiles().ToArray();
		NotifyStateChanged();
	}

	public async Task FlipTile(TileModel tile)
	{
		if (FlippedTiles!.Count < 2)
		{
			tile.IsShown = true;
			FlippedTiles.Add(tile);
		}
		else
		{
			return;
		}

		if (FlippedTiles!.Count == 2)
		{
			Moves += 1;
			if (FlippedTiles[0].MatchingId == FlippedTiles[1].MatchingId)
			{
				await Task.Delay(500);
				MatchedTiles!.AddRange(FlippedTiles);
				foreach (TileModel flippedTile in FlippedTiles)
				{
					flippedTile.IsMatched = true;
				}
			}
			else
			{
				await Task.Delay(1000);
				Misses += 1;
				foreach (TileModel flippedTile in FlippedTiles)
				{
					flippedTile.IsShown = false;
				}
			}
			FlippedTiles.Clear();
		}

		if (MatchedTiles!.Count == Tiles.Length)
		{
			GameInProgress = false;
			GameWon = true;
		}

		NotifyStateChanged();
	}

	public event Action? OnChange;
	private void NotifyStateChanged() => OnChange?.Invoke();
}
