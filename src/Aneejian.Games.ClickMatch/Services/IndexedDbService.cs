using Aneejian.Games.ClickMatch.Data;
using Microsoft.JSInterop;
using DbEnum = Aneejian.Games.ClickMatch.Enums.DbMethods;
using Aneejian.Games.ClickMatch.Helpers;
using System;

namespace Aneejian.Games.ClickMatch.Services;

public class IndexedDbService(IJSRuntime _jsRuntime) : IAsyncDisposable
{
	private readonly IJSRuntime _jsRuntime = _jsRuntime;
	private IJSObjectReference? _indexedDbRef;

	private async Task EnsureIndexedDbInitializedAsync()
	{
		_indexedDbRef ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Aneejian.Games.ClickMatch/js/indexedDb.js");
	}

	private async Task<T> InvokeAsync<T>(DbEnum method, params object[] args)
	{
		await EnsureIndexedDbInitializedAsync();
		return await _jsRuntime.InvokeAsync<T>(method.GetMethod(), args);
	}

	private async Task InvokeAsync(DbEnum method, params object[] args)
	{
		await EnsureIndexedDbInitializedAsync();
		await _jsRuntime.InvokeVoidAsync(method.GetMethod(), args);
	}

	public async Task<int> AddNewUser(UserDto user) => await InvokeAsync<int>(DbEnum.addNewUser, user);

	public async Task<UserDto> GetUser(int id) => await InvokeAsync<UserDto>(DbEnum.getUser, id);

	public async Task<UserDto> GetUser(string userName) => await InvokeAsync<UserDto>(DbEnum.getUser, userName);

	public async Task<UserDto> GetUser<T>(T idOrUserName) => await InvokeAsync<UserDto>(DbEnum.getUser, idOrUserName!);

	public async Task<IEnumerable<UserDto>> GetUsers() => await InvokeAsync<IEnumerable<UserDto>>(DbEnum.getUsers);

	public async Task DeleteUser(int id) => await InvokeAsync(DbEnum.deleteUser, id);

	public async Task DeleteUser(string userName) => await InvokeAsync(DbEnum.deleteUser, userName);

	public async Task<bool> UserExists(string userName) => await InvokeAsync<bool>(DbEnum.userExists, userName);

	public async Task<int> AddUserGame(GameDto game) => await InvokeAsync<int>(DbEnum.addUserGame, game);

	public async ValueTask DisposeAsync()
	{
		if (_indexedDbRef != null)
		{
			await _indexedDbRef.DisposeAsync();
		}
	}
}
