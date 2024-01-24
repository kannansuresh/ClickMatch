using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Data;

public class GameDto
{
    [Key]
    public int Id { get; set; }


    [Required]
    // foreign key for User ID
    public int UserId { get; set; }
    public UserDto User { get; set; } = null!;

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
