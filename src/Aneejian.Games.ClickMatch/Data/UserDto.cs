using System.ComponentModel.DataAnnotations;
using System;
using static System.Net.WebRequestMethods;

namespace Aneejian.Games.ClickMatch.Data;

public class UserDto
{

	private string _userName = string.Empty;

	[Key]
	public int Id { get; set; }

	[Required(ErrorMessage = "User Name is required."), StringLength(20, ErrorMessage = "User Name length should be between 3 and 20.", MinimumLength = 3)]
	[RegularExpression(@"^[a-zA-Z0-9_]{3,20}$", ErrorMessage = "User name cannot contain special characters other than under score.")]
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

	[Required(ErrorMessage = "Display Name is required."), StringLength(20, ErrorMessage = "Display Name length should be between 3 and 20.", MinimumLength = 3)]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Password is required.")]
	public string Password { get; set; } = string.Empty;

	[Required(ErrorMessage = "Avatar is required.")]
	public string? Avatar { get; set; }

}
