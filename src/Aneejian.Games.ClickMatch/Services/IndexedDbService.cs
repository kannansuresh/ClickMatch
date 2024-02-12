using Aneejian.Games.ClickMatch.Constants;
using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Helpers;
using Microsoft.JSInterop;
using DbEnum = Aneejian.Games.ClickMatch.Enums.DbMethods;

namespace Aneejian.Games.ClickMatch.Services;

public class IndexedDbService(IJSRuntime _jsRuntime) : IAsyncDisposable
{
	private readonly IJSRuntime _jsRuntime = _jsRuntime;
	private IJSObjectReference? _dexieRef;
	private IJSObjectReference? _module;
	private IJSObjectReference? _indexedDbRef;

	private async Task EnsureIndexedDbInitializedAsync()
	{
		if (_indexedDbRef != null)
			return;
		_dexieRef ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", AppStrings.JSFiles.Dexie);
		_module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", AppStrings.JSFiles.IndexedDb);
		_indexedDbRef = await _module.InvokeAsync<IJSObjectReference>("createIndexedDb");
	}

	private async Task<T> InvokeAsync<T>(DbEnum method, params object[] args)
	{
		await EnsureIndexedDbInitializedAsync();
		var result = await _indexedDbRef!.InvokeAsync<T>(method.GetEnumString(), args);
		return result;
	}

	private async Task InvokeAsync(DbEnum method, params object[] args)
	{
		await EnsureIndexedDbInitializedAsync();
		await _indexedDbRef!.InvokeVoidAsync(method.GetEnumString(), args);
	}

	public async Task<int> AddNewUser(UserDto user) => await InvokeAsync<int>(DbEnum.addNewUser, user);

	public async Task<UserDto> GetUser(int id) => await InvokeAsync<UserDto>(DbEnum.getUser, id);

	public async Task<UserDto> GetUser(string userName) => await InvokeAsync<UserDto>(DbEnum.getUser, userName);

	public async Task<UserDto> GetUser<T>(T idOrUserName) => await InvokeAsync<UserDto>(DbEnum.getUser, idOrUserName!);

	public async Task<IEnumerable<UserDto>> GetUsers() => await InvokeAsync<IEnumerable<UserDto>>(DbEnum.getUsers);

	public async Task DeleteUser(int id) => await InvokeAsync(DbEnum.deleteUser, id);

	public async Task DeleteUser(string userName) => await InvokeAsync(DbEnum.deleteUser, userName);

	public async Task<bool> UserExists(string userName) => await InvokeAsync<bool>(DbEnum.userExists, userName);

	public async Task<int> GetUserMaxGameLevel(int userId) => await InvokeAsync<int>(DbEnum.getUserMaxGameLevel, userId);

	public async Task<int> AddUserGame(GameDto game) => await InvokeAsync<int>(DbEnum.addUserGame, game);

	public async Task UpdateUserGame(GameDto game) => await InvokeAsync(DbEnum.updateUserGame, game);

	public async Task<IEnumerable<LevelDTO>> GetUserStats(int id) => await InvokeAsync<IEnumerable<LevelDTO>>(DbEnum.getUserStats, id);

	public async ValueTask DisposeAsync()
	{
		if (_indexedDbRef != null)
			await _indexedDbRef.DisposeAsync();
		if (_module != null)
			await _module.DisposeAsync();
		if (_dexieRef != null)
			await _dexieRef.DisposeAsync();
	}
}