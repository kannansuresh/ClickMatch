namespace Aneejian.Games.ClickMatch.Services;

using Aneejian.Games.ClickMatch.Data;
using DbEnum = Enums.DbMethods;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using Aneejian.Games.ClickMatch.Helpers;
using System.Reflection;
using System;
using System.Resources;

public class IndexedDbInterop(IJSRuntime jsRuntime)
{
	private readonly IJSRuntime _jsRuntime = jsRuntime;

	IJSObjectReference? _module = null;

	private async Task<T> InvokeAsync<T>(DbEnum method, params object[] args)
	{

		if (_module is null)
		{
			var jsFile = Assembly.GetExecutingAssembly().GetManifestResourceStream("Aneejian.Games.ClickMatch.Services.IndexedDbInterop.cs.js");
			var jsFileContent = await GetFileContentAsync(jsFile);
			_module ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/IndexedDb.js");
		}		
		return await _module.InvokeAsync<T>(method.GetMethod(), args);
	}

	private static async Task<string> GetFileContentAsync(Stream? file)
	{
		if (file is null)
			return string.Empty;
		using StreamReader reader = new(file);
		return await reader.ReadToEndAsync();
	}

	public async Task<int> AddNewUser(UserDto user) => await InvokeAsync<int>(DbEnum.addNewUser, user);

	public async Task<int> AddUserGame(GameDto game) => await InvokeAsync<int>(DbEnum.addUserGame, game);

	public async Task<IEnumerable<UserDto>> GetUsers() => await InvokeAsync<IEnumerable<UserDto>>(DbEnum.getUsers);

	public async Task<UserDto> GetUser(string userName) => await InvokeAsync<UserDto>(DbEnum.getUser, userName);

	public async Task<UserDto> GetUser(int id) => await InvokeAsync<UserDto>(DbEnum.getUser, id);

	public async Task<bool> UserExists(string userName) => await InvokeAsync<bool>(DbEnum.userExists, userName);
}
