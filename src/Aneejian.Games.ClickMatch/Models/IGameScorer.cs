namespace Aneejian.Games.ClickMatch.Models
{
	public interface IGameScorer
	{
		int Bonus { get; }
		int Score { get; }
		int TotalScore { get; }
		int Multiplier { get; set; }

		void CalculateScore(IEnumerable<TileModel> flippedTiles, int totalTiles);
		void ModifyScore(int score);
		void Reset();
	}
}