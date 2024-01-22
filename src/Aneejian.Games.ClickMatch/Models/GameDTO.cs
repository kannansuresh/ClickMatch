using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Models;

public class GameDTO
{
	[Key]
	public int Id { get; set; }


	[Required]
	// foreign key for User ID
	public int UserId { get; set; }
	public UserDTO User { get; set; } = null!;

	[Required]
	public int Level { get; set; }

	[Required]
	public int Score { get; set; }

	[Required]
	public double TimeTaken { get; set; }

	[Required]
	public bool GameWon { get; set; }

	[Required]
	public bool GameAbandoned { get; set; }
}
