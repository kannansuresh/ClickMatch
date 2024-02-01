using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Security;

namespace Aneejian.Games.ClickMatch.Services.Authentication;

public class AuthenticationService(IndexedDbService indexedDbService, INotifyAuthenticationStateChanged authenticationStateChanged)
{
	private bool _isAuthenticated;
	private readonly INotifyAuthenticationStateChanged _authenticationStateChanged = authenticationStateChanged;
	private string? _authenticationErrorMessage;
	private readonly IndexedDbService _indexedDbService = indexedDbService;

	public bool IsAuthenticated
	{
		get => _isAuthenticated;
		set
		{
			_isAuthenticated = value;
			NotifyAuthenticationStateChanged();
		}
	}
	public string AuthenticationErrorMessage => _authenticationErrorMessage ?? "";

	public async Task<bool> Login(string userName, string password)
	{
		try
		{
			var user = await GetUser(userName);
			return Login(user, password);
		}
		catch (Exception ex)
		{
			_authenticationErrorMessage = $"Login failed. {ex.Message}";
			return false;
		}
	}

	public async Task<bool> Login(int userId, string password)
	{
		try
		{
			var user = await GetUser(userId);
			return Login(user, password);
		}
		catch (Exception ex)
		{
			_authenticationErrorMessage = $"Login failed. {ex.Message}";
			return false;
		}
	}

	public bool Login(UserDto user, string password)
	{
		try
		{
			if (PasswordManager.ValidatePassword(password, user.Password))
			{
				_authenticationErrorMessage = "";
				IsAuthenticated = true;
				return true;
			}
			else
			{
				_authenticationErrorMessage = "Password is incorrect.";
				return false;
			}
		}
		catch (Exception ex)
		{
			_authenticationErrorMessage = $"Login failed. {ex.Message}";
			return false;
		}
	}

	public void Logout()
	{
		IsAuthenticated = false;
	}

	private async Task<UserDto> GetUser<T>(T userIdOrName)
	{
		var user = await _indexedDbService.GetUser(userIdOrName);
		return user;
	}

	private void NotifyAuthenticationStateChanged()
	{
		_authenticationStateChanged.OnAuthenticationStateChanged();
	}

}
