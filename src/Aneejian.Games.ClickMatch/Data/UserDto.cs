using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Data;

public class UserDto
{
	private string _userName = string.Empty;

	[Key]
	public int Id { get; set; }

	[Required(ErrorMessage = "User Name is required.")]
	[StringLength(20, ErrorMessage = "The User Name must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
	[RegularExpression(@"^[a-zA-Z0-9_]{0,20}$", ErrorMessage = "User name cannot contain special characters other than under score.")]
	public string UserName
	{
		get
		{
			return _userName;
		}
		set
		{
			_userName = value.Trim().ToLower();
		}
	}

	[Required(ErrorMessage = "Display Name is required.")]
	[StringLength(20, ErrorMessage = "The Display Name must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Password is required.")]
	// the max value for string length is purposefully set to int.MaxValue as the encryption will increase the length of the password
	[StringLength(int.MaxValue, ErrorMessage = "The Password must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
	public virtual string Password { get; set; } = string.Empty;

	[Required(ErrorMessage = "Avatar is required.")]
	[Url(ErrorMessage = "Avatar should be a valid URL.")]
	public string? Avatar { get; set; }
}