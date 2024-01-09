﻿namespace Aneejian.Games.ClickMatch.Models;

public class TileModel(Guid matchingId, int? positionId, string? content, bool showNumber)
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public Guid MatchingId { get; set; } = matchingId;
	public int? PositionId { get; set; } = positionId;
	public bool ShowNumber { get; set; } = showNumber;
	public string? Content { get; set; } = content;
	public bool IsShown { get; set; } = false;
	public bool IsMatched { get; set; } = false;

	private static readonly HashSet<string> imageFormats = new(StringComparer.OrdinalIgnoreCase)
	{
		"jpeg", "jpg", "png", "gif", "webp", "svg", "tiff", "bmp"
	};

	public bool ContentIsImage()
	{
		if (string.IsNullOrWhiteSpace(Content))
			return false;

		var extension = Content.LastIndexOf('.') >= 0
			? Content[(Content.LastIndexOf('.') + 1)..].Trim().ToLowerInvariant()
			: null;

		return extension != null && imageFormats.Contains(extension);
	}
}
