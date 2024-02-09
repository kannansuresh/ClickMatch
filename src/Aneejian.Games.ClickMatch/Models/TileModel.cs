using Aneejian.Games.ClickMatch.Helpers;

namespace Aneejian.Games.ClickMatch.Models;

public class TileModel(Guid matchingId, int? positionId, string? content)
{
	public Guid Id { get; set; } = Guid.NewGuid();
	public Guid MatchingId { get; set; } = matchingId;
	public int? PositionId { get; set; } = positionId;
	public string? Content { get; set; } = content;
	public string? ContentWebp => Content?.ToWebp() ?? "";
	public bool IsShown { get; set; } = false;
	public int FlipCount { get; set; } = 0;
	public bool IsMatched { get; set; } = false;
	public bool IsImage => Content.IsImage();

}
