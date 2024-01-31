using Microsoft.JSInterop;

namespace Aneejian.Games.ClickMatch.Services
{
	public class SessionStorageService(IJSRuntime jsRuntime) : IAsyncDisposable
	{
		private IJSObjectReference? _storage;
		private readonly IJSRuntime _jsRuntime = jsRuntime;

		private async Task EnsureStorageInitializedAsync()
		{
			_storage ??= await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/sessionStorage.js");
		}

		public async Task<T> GetValueAsync<T>(string key)
		{
			await EnsureStorageInitializedAsync();
			return await _storage!.InvokeAsync<T>("get", key);
		}

		public async Task SetValueAsync<T>(string key, T value)
		{
			await EnsureStorageInitializedAsync();
			await _storage!.InvokeVoidAsync("set", key, value);
		}

		public async Task ClearAsync()
		{
			await EnsureStorageInitializedAsync();
			await _storage!.InvokeVoidAsync("clear");
		}

		public async Task RemoveAsync(string key)
		{
			await EnsureStorageInitializedAsync();
			await _storage!.InvokeVoidAsync("remove", key);
		}

		public async ValueTask DisposeAsync()
		{
			if (_storage != null)
			{
				await _storage.DisposeAsync();
			}
		}
	}
}
