using Aneejian.Games.ClickMatch.Constants;
using Aneejian.Games.ClickMatch.Data;

namespace Aneejian.Games.ClickMatch.Helpers;

public static class SharedMethods
{
	public static string WebpProfileImage(UserDto profile)
	{
		if (profile.Avatar is null)
		{
			return "";
		}
		if (profile.Avatar == AppStrings.GuestUser.Avatar)
		{
			return Path.ChangeExtension(profile.Avatar, "webp");
		}
		if (profile.Avatar.Contains("aneejian.com", StringComparison.CurrentCultureIgnoreCase) || profile.Avatar.Contains("kannansuresh", StringComparison.CurrentCultureIgnoreCase))
		{
			return profile.Avatar.ToWebp();
		}
		return profile.Avatar;
	}

	public static void Log(string message)
	{
#if DEBUG
		Console.WriteLine(message);
#endif
	}
}