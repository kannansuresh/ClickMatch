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

	public async Task<IEnumerable<IThemeData>> GetThemesAsync(string localThemeInfo, string hostedThemeInfo)
	{
		IEnumerable<IThemeData>? localThemes;
		IEnumerable<IThemeData>? hostedThemes;

		var themes = new HashSet<IThemeData>();

		try
		{

			hostedThemes = await GetAsync<IEnumerable<ThemeData>>(hostedThemeInfo, true);
			if (hostedThemes != null)
				themes.UnionWith(hostedThemes);
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync(ex.Message);
			try
			{
				localThemes = await GetAsync<IEnumerable<ThemeData>>(localThemeInfo, true);
				if (localThemes != null)
					themes.UnionWith(localThemes);
			}
			catch (Exception ex2)
			{
				await Console.Out.WriteLineAsync(ex2.Message);

			}
		}

		return themes;
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
