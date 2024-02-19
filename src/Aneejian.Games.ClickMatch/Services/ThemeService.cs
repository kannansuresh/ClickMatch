using Aneejian.Games.ClickMatch.Helpers;
using Aneejian.Games.ClickMatch.Models;
using System.Text.Json;

namespace Aneejian.Games.ClickMatch.Services;

public class ThemeService(HttpClient httpClient, string configPath) : IThemeService
{
	private readonly HttpClient httpClient = httpClient;
	private readonly string configPath = configPath;

	public Config? ThemeConfig { get; set; }
	public IEnumerable<IThemeData>? ThemeDatas { get; set; }

	public bool IsInitialized => ThemeConfig != null && ThemeDatas != null;

	public async Task InitializeAsync()
	{
		if (ThemeDatas != null) return;
		var themeConfig = await GetConfigAsync(configPath);
		var themeDatas = await GetThemesAsync(themeConfig.LocalThemeInfo, themeConfig.HostedThemeInfo);
		ThemeConfig = themeConfig;
		ThemeDatas = themeDatas;
	}

	private async Task<Config> GetConfigAsync(string configPath)
	{
		var config = await GetAsync<Config>(configPath, true);
		return config ?? throw new Exception("Error getting config.");
	}

	private async Task<IEnumerable<IThemeData>> GetThemesAsync(string localThemeInfo, string hostedThemeInfo)
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
			SharedMethods.Log(ex.ToString());
			try
			{
				localThemes = await GetAsync<IEnumerable<ThemeData>>(localThemeInfo, true);
				if (localThemes != null)
					themes.UnionWith(localThemes);
			}
			catch (Exception ex2)
			{
				SharedMethods.Log(ex2.ToString());
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