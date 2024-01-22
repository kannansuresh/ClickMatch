using Aneejian.Games.ClickMatch.Models;

namespace Aneejian.Games.ClickMatch.Services;

public class GameManagerService
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public IGameSettings GameSettings { get; set; } = null!;
	public IGameScorer GameScorer { get; set; } = null!;
	public TileModel[] Tiles { get; set; } = [];

	public int Moves { get; set; }
	public int Misses { get; set; }
	public bool GameInProgress { get; set; }
	public bool GameWon { get; set; }
	public bool GameLost { get; set; }

	public int Multiplier { get; set; } = 1;

	private List<TileModel>? FlippedTiles { get; set; } = [];
	private List<TileModel>? MatchedTiles { get; set; } = [];

	public void StartGame(IGameSettings gameSettings)
	{
		GameSettings = gameSettings ?? throw new Exception("Game settings not set.");
		GameScorer ??= new GameScorer();
		GameScorer.Reset();
		Tiles = GameSettings.GenerateTiles().ToArray();
		GameInProgress = true;
	}

	public void ResetGame()
	{
		FlippedTiles = [];
		MatchedTiles = [];
		Moves = 0;
		Misses = 0;
		Multiplier = 1;
		GameWon = false;
		GameLost = false;
		GameInProgress = false;
		Tiles = [];
		GameScorer.Reset();
		NotifyStateChanged();
	}

	public async Task FlipTile(TileModel tile)
	{
		if (FlippedTiles!.Count >= 2) return;

		tile.IsShown = true;
		tile.FlipCount += 1;
		FlippedTiles.Add(tile);

		if (FlippedTiles!.Count == 2)
		{
			Moves += 1;

			if (FlippedTiles[0].MatchingId == FlippedTiles[1].MatchingId)
			{
				await Task.Delay(500);
				MatchedTiles!.AddRange(FlippedTiles);
				GameScorer.CalculateScore(FlippedTiles, Tiles.Length);
				FlippedTiles.ConvertAll(t => t.IsMatched = true);
				Multiplier += Misses < Tiles.Length ? 1 : 0;
			}
			else
			{
				Misses += 1;
				await Task.Delay(1000);
				FlippedTiles.ConvertAll(t => t.IsShown = false);
				Multiplier = 1;
			}
			FlippedTiles.Clear();
		}

		if (MatchedTiles!.Count == Tiles.Length)
		{
			GameInProgress = false;
			GameWon = GameScorer.Score > 0;
			GameLost = !GameWon;
		}
		GameScorer.Multiplier = Multiplier;
		NotifyStateChanged();
	}


	public event Action? OnChange;

	private void NotifyStateChanged() => OnChange?.Invoke();
}
