namespace Aneejian.Games.ClickMatch.Data;

public class LevelDTO
{
	public int Level { get; set; }
	public int HighScore { get; set; }
	public int TimesPlayed { get; set; }
	public int TimesWon { get; set; }
	public int TimesLost { get; set; }
	public int TimesAbandoned => TimesPlayed - (TimesWon + TimesLost);
}
