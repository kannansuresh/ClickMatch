using System.ComponentModel.DataAnnotations;
using System;
using static System.Net.WebRequestMethods;

namespace Aneejian.Games.ClickMatch.Data;

public class UserDto
{

	private string _userName = string.Empty;
	private string _avatar = string.Empty;

	[Key]
	public int Id { get; set; }

	[Required(ErrorMessage = "User Name is required.")]
	[RegularExpression(@"^[a-zA-Z0-9_]{3,20}$", ErrorMessage = "User name should be more than 3 characters long and cannot contain special characters other than under score.")]
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
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Password is required.")]
	public string Password { get; set; } = string.Empty;

	public string Avatar
	{
		get
		{
			return GetAvatar();
		}
		set
		{
			_avatar = value.Trim();
		}

	}

	private string GetAvatar()
	{
		if (string.IsNullOrWhiteSpace(_avatar) && !Uri.IsWellFormedUriString(_avatar, UriKind.RelativeOrAbsolute))
			_avatar = GetRandomImage();
		return _avatar;
	}

	private static string GetRandomImage()
	{
		var random = new Random();
		var randomImageIndex = random.Next(1, 380);
		return $"https://aneejian.com/memory-game-resources/images/animal_avatars_circular/{randomImageIndex}-min.png";
	}

}
