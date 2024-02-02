using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Security;

namespace Aneejian.Games.ClickMatch.Helpers;

public static class ExtensionMethods
{
	public static string GetEnumString<T>(this T enumValue) where T : Enum
	{
		return enumValue.ToString();
	}

	public static string HashUserDto(this UserDto user, string loginRequestId)
	{
		return PasswordManager.HashPassword(user.PlainHashUserDto(loginRequestId));
	}

	public static string PlainHashUserDto(this UserDto user, string loginRequestId)
	{
		return $"{user.Id}{user.UserName}{loginRequestId}";
	}
}
