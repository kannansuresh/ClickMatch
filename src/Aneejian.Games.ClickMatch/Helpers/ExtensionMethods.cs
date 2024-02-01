using Aneejian.Games.ClickMatch.Enums;

namespace Aneejian.Games.ClickMatch.Helpers;

public static class ExtensionMethods
{
    public static string GetEnumString<T>(this T enumValue) where T : Enum
    {
        return enumValue.ToString();
    }
}
