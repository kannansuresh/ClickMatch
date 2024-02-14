namespace Aneejian.Games.ClickMatch.Data;

public class LevelStatsDTO
{
	public int UserId { get; set; }
	public int Level { get; set; }
	public int HighScore { get; set; }
	public int TimesPlayed { get; set; }
	public int TimesWon { get; set; }
	public int TimesLost { get; set; }
	public int TimesAbandoned => TimesPlayed - (TimesWon + TimesLost);
	public GameDto? UsersBestGame { get; set; }
	public GameDto? LevelsBestGame { get; set; }
}
