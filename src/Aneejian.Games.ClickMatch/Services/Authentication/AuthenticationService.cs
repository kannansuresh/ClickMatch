using Aneejian.Games.ClickMatch.Constants;
using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Helpers;
using Aneejian.Games.ClickMatch.Security;

namespace Aneejian.Games.ClickMatch.Services.Authentication;

public class AuthenticationService(IndexedDbService indexedDbService, SessionStorageService sessionStorageService)
{
	private bool _isAuthenticated;
	private string? _authenticationErrorMessage;
	private readonly IndexedDbService _indexedDbService = indexedDbService;
	private readonly SessionStorageService _sessionStorageService = sessionStorageService;

	public event Action<bool>? OnAuthenticationStateChanged;

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
			return await Login(user, password);
		}
		catch (Exception ex)
		{
			_authenticationErrorMessage = $"{ex.Message}";
			IsAuthenticated = false;
			return false;
		}
	}

	public async Task<bool> Login(int userId, string password)
	{
		try
		{
			var user = await GetUser(userId);
			return await Login(user, password);
		}
		catch (Exception ex)
		{
			_authenticationErrorMessage = $"{ex.Message}";
			IsAuthenticated = false;
			return false;
		}
	}

	public async Task<bool> ValidateLoggedInUser(UserDto user) { 
		var loginRequestId = await _sessionStorageService.GetValueAsync<string>(AppStrings.SessionStorageKeys.LoginRequestId);
		var token = await _sessionStorageService.GetValueAsync<string>(AppStrings.SessionStorageKeys.Token);
		var checkToken = PasswordManager.ValidatePassword(user.PlainHashUserDto(loginRequestId), token);
		if (checkToken)
		{
			_authenticationErrorMessage = "";
			IsAuthenticated = true;
			return true;
		}
		else
		{
			IsAuthenticated = false;
			_authenticationErrorMessage = "User is not logged in.";
			return false;
		}
	}

	public async Task<bool> Login(UserDto user, string password)
	{
		try
		{
			if (PasswordManager.ValidatePassword(password, user.Password))
			{
				_authenticationErrorMessage = "";
				IsAuthenticated = true;
				await _sessionStorageService.SetValueAsync(AppStrings.SessionStorageKeys.UserId, user.Id);
				var loginRequestId = Guid.NewGuid().ToString();
				await _sessionStorageService.SetValueAsync(AppStrings.SessionStorageKeys.LoginRequestId, loginRequestId);
				await _sessionStorageService.SetValueAsync(AppStrings.SessionStorageKeys.Token, user.HashUserDto(loginRequestId));
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
			_authenticationErrorMessage = $"{ex.Message}";
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
		return user ?? throw new Exception("User does not exist.");
	}

	private void NotifyAuthenticationStateChanged() => OnAuthenticationStateChanged?.Invoke(IsAuthenticated);
}
