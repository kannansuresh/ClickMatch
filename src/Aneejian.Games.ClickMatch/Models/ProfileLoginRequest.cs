using Aneejian.Games.ClickMatch.Constants;
using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Security;
using Aneejian.Games.ClickMatch.Services;
using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Models;

public class ProfileLoginRequest(SessionStorageService sessionStorageService)
{
	private readonly SessionStorageService _sessionStorageService = sessionStorageService;

	[Required(ErrorMessage = "Password is required.")]
	public string Password { get; set; } = "";

	public string? ErrorMessage { get; set; }

	public async Task<bool> LoginProfile(UserDto profile)
	{
		if (string.IsNullOrEmpty(Password) && profile.UserName != AppStrings.GuestUser.UserName)
			return false;

		if (PasswordManager.ValidatePassword(Password, profile.Password) || profile.UserName == AppStrings.GuestUser.UserName)
		{
			ErrorMessage = "";
			await _sessionStorageService.SetValueAsync("profileId", profile.Id);
		}
		else
		{
			ErrorMessage = "Password is incorrect.";
			return false;
		}
		return true;
	}

	public async Task<bool> LogoutProfile()
	{
		await _sessionStorageService.RemoveAsync("profileId");
		return true;
	}
}