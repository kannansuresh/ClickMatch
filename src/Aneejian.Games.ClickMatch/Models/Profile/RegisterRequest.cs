using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Security;
using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Models.Profile;

public class RegisterRequest : UserDto
{
	public override string Password
	{
		get
		{
			return base.Password;
		}
		set
		{
			base.Password = PasswordManager.HashPassword(InputPassword);
		}
	}

	[Required(ErrorMessage = "Password is required.")]
	public string InputPassword { get; set; } = string.Empty;


	[Required(ErrorMessage = "Password confirmation is required.")]
	[Compare(nameof(InputPassword), ErrorMessage = "Password and Confirm Password do not match.")]
	public string ConfirmPassword { get; set; } = string.Empty;

}
