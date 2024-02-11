using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Models;
using Aneejian.Games.ClickMatch.Services.Authentication;

namespace Aneejian.Games.ClickMatch.Services;

public class GameManagerService(AuthenticationService authenticationService, IndexedDbService indexedDbService)
{
	private readonly AuthenticationService _authenticationService = authenticationService;
	private readonly IndexedDbService _indexedDbService = indexedDbService;

	public IGameSettings GameSettings { get; set; } = null!;
	public IGameScorer GameScorer { get; set; } = null!;
	public TileModel[] Tiles { get; set; } = [];

	public UserDto? Player { get; set; }

	public int Moves { get; set; }
	public int Misses { get; set; }
	public bool GameInProgress { get; set; }
	public bool GameWon { get; set; }
	public bool GameLost { get; set; }

	private int IndexedDbGameId { get; set; }
	private GameDto? GameData { get; set; }

	public int Multiplier { get; set; } = 1;

	private List<TileModel>? FlippedTiles { get; set; } = [];
	private List<TileModel>? MatchedTiles { get; set; } = [];

	public async Task StartGame(IGameSettings gameSettings)
	{
		Player = _authenticationService.AuthenticatedUser;
		GameSettings = gameSettings ?? throw new Exception("Game settings not set.");
		GameScorer ??= new GameScorer();
		GameScorer.Reset();
		Tiles = GameSettings.GenerateTiles().ToArray();
		GameInProgress = true;
		await SaveGameData();
		NotifyStateChanged();
	}

	private async Task SaveGameData()
	{
		GameDto gameData = new()
		{
			UserId = Player!.Id,
			Level = GameSettings.GameLevel,
			Score = GameScorer.Score,
			TimeTaken = 0,
			GameWon = GameWon,
			GameLost = GameLost
		};
		GameData = gameData;
		IndexedDbGameId = await _indexedDbService.AddUserGame(GameData);
		GameData.Id = IndexedDbGameId;
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
		GameData = null;
		IndexedDbGameId = 0;
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
			await SetGameResult();
		}
		GameScorer.Multiplier = Multiplier;
		NotifyStateChanged();
	}

	private async Task SetGameResult()
	{
		GameInProgress = false;
		GameWon = GameScorer.Score > 0;
		GameLost = !GameWon;
		GameData!.Score = GameScorer.TotalScore;
		GameData.TimeTaken = 0;
		GameData.GameWon = GameWon;
		GameData.GameLost = GameLost;
		await _indexedDbService.UpdateUserGame(GameData);
	}

	public event Action? OnChange;

	private void NotifyStateChanged()
	{
		OnChange?.Invoke();
	}
}