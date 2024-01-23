using Aneejian.Games.ClickMatch.Enums;

namespace Aneejian.Games.ClickMatch.Helpers;

internal static class ExtensionMethods
{
	internal static string GetMethod(this DbMethods method)
	{
		return $"iDbWrapper.{method}";
	}
}
