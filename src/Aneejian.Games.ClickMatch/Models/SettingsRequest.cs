using System.ComponentModel.DataAnnotations;

namespace Aneejian.Games.ClickMatch.Models;

public class SettingsRequest
{
	[Required]
	public int Level { get; set; }

	[Required]
	public string ThemeId { get; set; } = "";

	[Required]
	public bool ShowTileNumber { get; set; }
}