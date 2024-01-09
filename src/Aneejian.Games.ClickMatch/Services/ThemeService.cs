using System.Text.Json;

namespace Aneejian.Games.ClickMatch.Services;

public class ThemeService(HttpClient httpClient) : IThemeService
{
	private readonly HttpClient httpClient = httpClient;

	public async Task<T?> GetAsync<T>(string path, bool deserialize)
	{
		var response = await httpClient.GetAsync(path);

		if (!response.IsSuccessStatusCode)
			throw new Exception($"Error getting data: {response.StatusCode}");

		var content = await response.Content.ReadAsStringAsync();

		if (!deserialize)
			return (T?)Convert.ChangeType(content, typeof(T));

		JsonSerializerOptions jsonSerializerOptions = new()
		{
			PropertyNameCaseInsensitive = true
		};
		var options = jsonSerializerOptions;

		return JsonSerializer.Deserialize<T>(content, options)!;
	}
}
