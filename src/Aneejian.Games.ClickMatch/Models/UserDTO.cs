using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Models;

public class UserDTO
{
	[Key]
	public int Id { get; set; }

	[Required]
	public string Name { get; set; } = string.Empty;

	[Required]
	public  string UserName { get; set; } = string.Empty;

	[Required]
	public string Password { get; set; } = string.Empty;
}
