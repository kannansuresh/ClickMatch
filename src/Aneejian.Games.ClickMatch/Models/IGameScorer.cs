namespace Aneejian.Games.ClickMatch.Models
{
	public interface IGameScorer
	{
		int Bonus { get; set; }
		int Score { get; set; }
		int TotalScore { get; }
		int Multiplier { get; set; }

		void CalculateScore(int numberOfFlips, int totalTiles);
		void ModifyScore(int score);
		void Reset();
	}
}