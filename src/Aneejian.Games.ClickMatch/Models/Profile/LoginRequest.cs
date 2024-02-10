using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Models.Profile;

public class LoginRequest
{
	[Required(ErrorMessage = "User name is required.")]
	public string UserName { get; set; } = "";

	[Required(ErrorMessage = "Password is required.")]
	public string Password { get; set; } = "";
}