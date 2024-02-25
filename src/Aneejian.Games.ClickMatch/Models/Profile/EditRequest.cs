using Aneejian.Games.ClickMatch.Data;
using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Models.Profile;

public class EditRequest : RegisterRequest
{
	[Required(ErrorMessage = "Current password is required.")]
	public string CurrentPassword { get; set; } = string.Empty;

	public EditRequest(UserDto user)
	{
		Id = user.Id;
		UserName = user.UserName;
		Name = user.Name;
		Avatar = user.Avatar;
		Password = user.Password;
	}

}
