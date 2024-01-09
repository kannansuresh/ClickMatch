using System.Globalization;
using System.Text.Json.Serialization;

namespace Aneejian.Games.ClickMatch.Models;

public class ThemeData : IThemeData
{
	[JsonPropertyName("theme_url")]
	public string? ThemeUrl { get; set; }

	[JsonPropertyName("theme_type")]
	public string? ThemeType { get; set; }

	[JsonPropertyName("theme_id")]
	public string? ThemeId { get; set; }

	[JsonPropertyName("item_count")]
	public int ItemCount { get; set; }

	[JsonPropertyName("item_names")]
	public IEnumerable<string>? ItemNames { get; set; }

	[JsonPropertyName("more_info")]
	public Dictionary<string, dynamic>? MoreInfo { get; set; }

	public string ThemeName => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(ThemeId?.Replace("_", " ") ?? string.Empty);

	public string Attribution => GetMoreInfo<string>("attribution");

	public bool AttributionRequired => GetMoreInfo<bool>("attribution_required");

	public string Difficulty => GetMoreInfo<string>("difficulty");

	private T GetMoreInfo<T>(string key)
	{
		if (MoreInfo == null)
			return default!;

		MoreInfo.TryGetValue(key, out var value);

		var valueToConvert = value?.ToString() ?? string.Empty;

		if (string.IsNullOrEmpty(valueToConvert))
			return default!;

		return (T)Convert.ChangeType(valueToConvert, typeof(T));
	}

	public IEnumerable<string> GetItems(int itemsToReturn)
	{
		if (ItemCount < itemsToReturn)
			throw new ArgumentOutOfRangeException($"Not enough images in theme {ThemeName} to play the game.");

		string[] itemNames = ItemNames?.ToArray() ?? [];

		var uniqueItems = new HashSet<string>(itemsToReturn);

		while (uniqueItems.Count < itemsToReturn)
		{
			var randomIndex = Random.Shared.Next(ItemCount);
			uniqueItems.Add(itemNames[randomIndex]);

			if (uniqueItems.Count == itemsToReturn)
				break;
		}

		var formattedItemUrl = ThemeUrl + "{0}";

		return uniqueItems.Select(image => string.Format(formattedItemUrl, image));
	}

}
