using System.ComponentModel.DataAnnotations;
using Aneejian.Games.ClickMatch.Constants;
using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Security;
using Aneejian.Games.ClickMatch.Services;

namespace Aneejian.Games.ClickMatch.Models;

public class ProfileDeleteRequest(IndexedDbService indexedDbService)
{

	readonly IndexedDbService _indexedDbService = indexedDbService;

	[Required(ErrorMessage = "Password is required.")]
	public string ConfirmationPassword { get; set; } = "";

	public string? ErrorMessage { get; set; }

	public async Task<bool> DeleteProfile(UserDto profile)
	{
		if (string.IsNullOrEmpty(ConfirmationPassword))
			return false;

		if (PasswordManager.ValidatePassword(ConfirmationPassword, profile.Password))
		{
			await _indexedDbService.DeleteUser(profile.Id);
			ErrorMessage = "";
		}
		else
		{
			ErrorMessage = "Password is incorrect.";
			return false;
		}
		return true;
	}
}
