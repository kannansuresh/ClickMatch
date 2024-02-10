using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Data;

public class GameDto
{
	[Key]
	public int Id { get; set; }

	// foreign key for User ID
	public int UserId { get; set; }

	public UserDto User { get; set; } = null!;

	public int Level { get; set; }

	public int Score { get; set; }

	public double TimeTaken { get; set; }

	public bool GameWon { get; set; } = false;

	public bool GameLost { get; set; } = false;

	public bool GameAbandoned => !GameWon && !GameLost;
}