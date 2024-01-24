using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Data;

public class UserDto
{
	[Key]
	public int Id { get; set; }

	[Required(ErrorMessage = "User Name is required.")]
	[RegularExpression(@"^[a-zA-Z0-9_]{3,20}$", ErrorMessage = "User name should be more than 3 characters long and cannot contain special characters other than under score.")]
	public string UserName { get; set; } = string.Empty;

	[Required(ErrorMessage = "Display Name is required.")]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Password is required.")]
	public string Password { get; set; } = string.Empty;
}
