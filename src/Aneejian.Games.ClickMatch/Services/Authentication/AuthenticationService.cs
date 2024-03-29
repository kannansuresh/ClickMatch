﻿using Aneejian.Games.ClickMatch.Constants;
using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Helpers;
using Aneejian.Games.ClickMatch.Security;

namespace Aneejian.Games.ClickMatch.Services.Authentication;

public class AuthenticationService
{
	private bool _isAuthenticated;
	private string? _authenticationErrorMessage;
	private UserDto? _authenticatedUser;
	private readonly IndexedDbService _indexedDbService;
	private readonly SessionStorageService _sessionStorageService;

	public AuthenticationService(IndexedDbService indexedDbService, SessionStorageService sessionStorageService)
	{
		_indexedDbService = indexedDbService;
		_sessionStorageService = sessionStorageService;
		IsAuthenticated = false;
		IsSessionAuthenticated().ConfigureAwait(false);
	}

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

	public UserDto? AuthenticatedUser => _authenticatedUser ?? null;

	public string AuthenticationErrorMessage => _authenticationErrorMessage ?? "";

	public async Task<bool> Login(string userName, string password)
	{
		try
		{
			var user = await GetUserByUserName(userName);
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
			var user = await GetUserById(userId);
			return await Login(user, password);
		}
		catch (Exception ex)
		{
			_authenticationErrorMessage = $"{ex.Message}";
			IsAuthenticated = false;
			return false;
		}
	}

	public async Task<bool> IsSessionAuthenticated()
	{
		var loggedInUserId = await _sessionStorageService.GetValueAsync<string>(AppStrings.SessionStorageKeys.UserId);
		if (loggedInUserId != null)
		{
			var user = await GetUserById(Convert.ToInt32(loggedInUserId));
			return await ValidateLoggedInUser(user);
		}
		else
		{
			IsAuthenticated = false;
			return false;
		}
	}

	public async Task<bool> ValidateLoggedInUser(int userId)
	{
		var user = await GetUserById(userId);
		return await ValidateLoggedInUser(user);
	}

	public async Task<bool> ValidateLoggedInUser(UserDto user)
	{
		var loginRequestId = await _sessionStorageService.GetValueAsync<string>(AppStrings.SessionStorageKeys.LoginRequestId);
		var token = await _sessionStorageService.GetValueAsync<string>(AppStrings.SessionStorageKeys.Token);
		var checkToken = PasswordManager.ValidatePassword(user.PlainHashUserDto(loginRequestId), token);
		if (checkToken)
		{
			_authenticationErrorMessage = "";
			_authenticatedUser = user;
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
				await _sessionStorageService.SetValueAsync(AppStrings.SessionStorageKeys.UserId, user.Id);
				var loginRequestId = Guid.NewGuid().ToString();
				await _sessionStorageService.SetValueAsync(AppStrings.SessionStorageKeys.LoginRequestId, loginRequestId);
				await _sessionStorageService.SetValueAsync(AppStrings.SessionStorageKeys.Token, user.HashUserDto(loginRequestId));
				_authenticatedUser = user;
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
			_authenticationErrorMessage = $"{ex.Message}";
			return false;
		}
	}

	public async Task Logout()
	{
		await _sessionStorageService.RemoveAsync(AppStrings.SessionStorageKeys.UserId);
		await _sessionStorageService.RemoveAsync(AppStrings.SessionStorageKeys.LoginRequestId);
		await _sessionStorageService.RemoveAsync(AppStrings.SessionStorageKeys.Token);
		IsAuthenticated = false;
	}

	public async Task<UserDto> GetAuthenticatedUser()
	{
		if (!IsAuthenticated)
			throw new Exception("No user is authenticated.");
		var userId = await _sessionStorageService.GetValueAsync<string>(AppStrings.SessionStorageKeys.UserId);
		var user = await GetUserById(Convert.ToInt32(userId));
		var userIsLoggedIn = await ValidateLoggedInUser(user);
		if (userIsLoggedIn)
			return user;
		else
			throw new Exception("User is not logged in.");
	}

	private async Task<UserDto> GetUserById(int userId)
	{
		var user = await _indexedDbService.GetUserById(userId);
		return user ?? throw new Exception("User does not exist.");
	}

	private async Task<UserDto> GetUserByUserName(string userName)
	{
		var user = await _indexedDbService.GetUserByUserName(userName);
		return user ?? throw new Exception("User does not exist.");
	}

	private void NotifyAuthenticationStateChanged()
	{
		OnAuthenticationStateChanged?.Invoke(IsAuthenticated);
	}
}