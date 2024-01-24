using Aneejian.Games.ClickMatch.Data;
using Microsoft.JSInterop;
using DbEnum = Aneejian.Games.ClickMatch.Enums.DbMethods;
using Aneejian.Games.ClickMatch.Helpers;
using System;

namespace Aneejian.Games.ClickMatch.Client.Services;

public class IndexedDbService(IJSRuntime _jsRuntime)
{
    private readonly IJSRuntime _jsRuntime = _jsRuntime;

    private async Task<T> InvokeAsync<T>(DbEnum method, params object[] args)
    {
        return await _jsRuntime.InvokeAsync<T>(method.GetMethod(), args);
    }

    public async Task<int> AddNewUser(UserDto user) => await InvokeAsync<int>(DbEnum.addNewUser, user);

    public async Task<int> AddUserGame(GameDto game) => await InvokeAsync<int>(DbEnum.addUserGame, game);

    public async Task<IEnumerable<UserDto>> GetUsers() => await InvokeAsync<IEnumerable<UserDto>>(DbEnum.getUsers);

    public async Task<UserDto> GetUser(string userName) => await InvokeAsync<UserDto>(DbEnum.getUser, userName);

    public async Task<UserDto> GetUser(int id) => await InvokeAsync<UserDto>(DbEnum.getUser, id);

    public async Task<bool> UserExists(string userName) => await InvokeAsync<bool>(DbEnum.userExists, userName);
}
