using Aneejian.Games.ClickMatch.Models;
using System.Text.Json;

namespace Aneejian.Games.ClickMatch.Services;

public class ThemeService(HttpClient httpClient) : IThemeService
{
	private readonly HttpClient httpClient = httpClient;

	public async Task<Config> GetConfigAsync(string configPath)
	{
		var config = await GetAsync<Config>(configPath, true);
		return config ?? throw new Exception("Error getting config.");
	}

	public async Task<List<ThemeData>> GetThemesAsync(string localThemeInfo, string hostedThemeInfo)
	{
		List<ThemeData>? localThemes;
		List<ThemeData>? hostedThemes;

		var themes = new HashSet<ThemeData>();

		try
		{

			hostedThemes = await GetAsync<List<ThemeData>>(hostedThemeInfo, true);
			if (hostedThemes != null)
				themes.UnionWith(hostedThemes);
		}
		catch (Exception)
		{
			try
			{
				localThemes = await GetAsync<List<ThemeData>>(localThemeInfo, true);
				if (localThemes != null)
					themes.UnionWith(localThemes);
			}
			catch (Exception)
			{
				// ignore
			}
		}

		return [.. themes];
	}

	private async Task<T?> GetAsync<T>(string path, bool deserialize)
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
