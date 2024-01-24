using Aneejian.Games.ClickMatch.Enums;

namespace Aneejian.Games.ClickMatch.Helpers;

public static class ExtensionMethods
{
    public static string GetMethod(this DbMethods method)
    {
        return $"iDbWrapper.{method}";
    }
}
