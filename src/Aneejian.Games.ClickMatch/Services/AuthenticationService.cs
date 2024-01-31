using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Security;

namespace Aneejian.Games.ClickMatch.Services;

public class AuthenticationService(IndexedDbService indexedDbService, AuthStateProvider authStateProvider)
{
	private bool _isAuthenticated;
	private string? _authenticationErrorMessage;
	private readonly IndexedDbService _indexedDbService = indexedDbService;
	private readonly AuthStateProvider _authStateProvider = authStateProvider;

	public bool IsAuthenticated => _isAuthenticated;
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
				_isAuthenticated = true;
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
		_isAuthenticated = false;
		_authStateProvider.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
	}

	private async Task<UserDto> GetUser<T>(T userIdOrName)
	{
		var user = await _indexedDbService.GetUser(userIdOrName);
		return user;
	}

}
