namespace Aneejian.Games.ClickMatch.Models;

public class ScoreCollector
{
	public IEnumerable<IGameScorer>? GameScorers { get; set; }

	public int GetTotalScore()
	{
		return GameScorers?.Sum(x => x.TotalScore) ?? 0;
	}
}
