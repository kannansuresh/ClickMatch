using Microsoft.JSInterop;

namespace Aneejian.Games.ClickMatch.Services
{
	public class SessionStorageService(IJSRuntime jsRuntime) : IAsyncDisposable
	{
		private IJSObjectReference? _sessionStorageRef;
		private readonly IJSRuntime _jsRuntime = jsRuntime;

		private async Task EnsureStorageInitializedAsync()
		{
			_sessionStorageRef ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Aneejian.Games.ClickMatch/js/sessionStorage.js");
		}

		public async Task<T> GetValueAsync<T>(string key)
		{
			await EnsureStorageInitializedAsync();
			return await _sessionStorageRef!.InvokeAsync<T>("get", key);
		}

		public async Task SetValueAsync<T>(string key, T value)
		{
			await EnsureStorageInitializedAsync();
			await _sessionStorageRef!.InvokeVoidAsync("set", key, value);
		}

		public async Task ClearAsync()
		{
			await EnsureStorageInitializedAsync();
			await _sessionStorageRef!.InvokeVoidAsync("clear");
		}

		public async Task RemoveAsync(string key)
		{
			await EnsureStorageInitializedAsync();
			await _sessionStorageRef!.InvokeVoidAsync("remove", key);
		}

		public async ValueTask DisposeAsync()
		{
			if (_sessionStorageRef != null)
			{
				await _sessionStorageRef.DisposeAsync();
			}
		}
	}
}