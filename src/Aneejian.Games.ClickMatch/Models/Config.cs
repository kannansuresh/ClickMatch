using System.Text.Json.Serialization;

namespace Aneejian.Games.ClickMatch.Models;

public class Config
{
	[JsonPropertyName("local_theme_info")]
	public string LocalThemeInfo { get; set; } = string.Empty;

	[JsonPropertyName("hosted_theme_info")]
	public string HostedThemeInfo { get; set; } = string.Empty;

	[JsonPropertyName("default_theme")]
	public string DefaultTheme { get; set; } = string.Empty;

	[JsonPropertyName("default_level")]
	public int DefaultGameLevel { get; set; }
}
