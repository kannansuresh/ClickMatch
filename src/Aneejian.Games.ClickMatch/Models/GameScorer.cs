namespace Aneejian.Games.ClickMatch.Models;

public class GameScorer : IGameScorer
{
	private readonly int ScorePerFind = 10;
	private readonly int PerfectFindBonus = 2;
	private readonly int PenaltyForExtraFlips = 1;

	public int Score { get; set; } = 0;
	public int Bonus { get; set; } = 0;
	public int TotalScore => Score + Bonus;
	public int Multiplier { get; set; } = 1;

	public void Reset()
	{
		Score = 0;
		Bonus = 0;
		Multiplier = 1;
	}

	public void ModifyScore(int score)
	{
		Score += score;
	}

	public GameScorer()
	{
		Reset();
	}

	public GameScorer(int scorePerFind, int perfectFindBonus, int penaltyForExtraFlips)
	{
		ScorePerFind = scorePerFind;
		PerfectFindBonus = perfectFindBonus;
		PenaltyForExtraFlips = penaltyForExtraFlips;
		Reset();
	}

	public void CalculateScore(IEnumerable<TileModel> flippedTiles, int totalTiles)
	{
		int penaltyForExtraFlips = Math.Max(0, flippedTiles.Sum(t => t.FlipCount) - 2) * PenaltyForExtraFlips;
		double penaltyScale = GetPenaltyScale(totalTiles);
		var scoreForFind = (ScorePerFind * Multiplier) - (penaltyForExtraFlips * penaltyScale);
		Bonus += flippedTiles.Any(t => t.FlipCount == 1) ? PerfectFindBonus : 0;
		Score += Convert.ToInt32(scoreForFind);
	}

	public override string ToString()
	{
		return $"Score: {Score}, Bonus: {Bonus}, Multiplier: {Multiplier}x, Total: {TotalScore}";
	}

	private static int GetPenaltyScale(int totalTiles)
	{
		double penalty = Math.Pow(totalTiles, -0.5);
		double mappedPenalty = MapToRange(penalty, 0.1, 1.0);
		return Convert.ToInt32(mappedPenalty * 10);
	}

	private static double MapToRange(double value, double min, double max)
	{
		return Math.Max(min, Math.Min(max, value));
	}
}