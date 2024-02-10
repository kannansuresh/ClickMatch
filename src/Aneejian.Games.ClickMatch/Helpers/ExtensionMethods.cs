using Aneejian.Games.ClickMatch.Data;
using Aneejian.Games.ClickMatch.Security;
using System.Net;

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

	private static readonly HashSet<string> ImageFormats = new(StringComparer.OrdinalIgnoreCase)
	{
		".jpeg", ".jpg", ".png", ".gif", ".webp", ".svg", ".tiff", ".bmp"
	};

	public static bool IsImage(this string? content)
	{
		if (string.IsNullOrWhiteSpace(content))
			return false;

		var extension = content.Extension();
		if (string.IsNullOrWhiteSpace(extension))
			return false;

		var isImage = extension != null && ImageFormats.Contains(extension);
		return isImage;
	}

	public static string ToWebp(this string content)
	{
		if (string.IsNullOrWhiteSpace(content) || !content.IsImage())
			return string.Empty;

		var extension = content.Extension();
		if (extension == ".webp")
			return content;

		if (extension == ".svg")
			return "";

		var orginalFileName = Path.GetFileName(content);
		var webpFileName = Path.ChangeExtension(orginalFileName, "webp");
		var webpPath = content.Replace(orginalFileName, Path.Combine("webp/", webpFileName));

		return webpPath;
	}

	public static async Task<string> ToWebpAsync(this string content)
	{
		var webpPath = content.ToWebp();
		if (string.IsNullOrWhiteSpace(webpPath))
			return "";
		return await webpPath.CheckUrl() ? webpPath : "";
	}

	public static string Extension(this string content)
	{
		return Path.GetExtension(content);
	}

	public static async Task<bool> CheckUrl(this string url)
	{
		using var client = new HttpClient();
		try
		{
			var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
			return response.IsSuccessStatusCode;
		}
		catch (HttpRequestException)
		{
			return false; // Indicates a network or connection issue
		}
		catch (WebException)
		{
			return false; // Indicates a generic web exception
		}
	}
}