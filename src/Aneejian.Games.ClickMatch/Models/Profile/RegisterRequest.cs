using Aneejian.Games.ClickMatch.Constants;
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
	[StringLength(16, ErrorMessage = "The Password must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
	public string InputPassword { get; set; } = string.Empty;

	[Required(ErrorMessage = "Password confirmation is required.")]
	[Compare(nameof(InputPassword), ErrorMessage = "Password and Confirm Password do not match.")]
	public string ConfirmPassword { get; set; } = string.Empty;

	public RegisterRequest AddGuestUser()
	{
		UserName = AppStrings.GuestUser.UserName;
		Name = AppStrings.GuestUser.DisplayName;
		InputPassword = AppStrings.GuestUser.Password;
		Avatar = AppStrings.GuestUser.Avatar;
		Password = InputPassword;
		return this;
	}
}