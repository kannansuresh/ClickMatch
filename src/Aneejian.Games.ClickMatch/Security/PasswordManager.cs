namespace Aneejian.Games.ClickMatch.Security;

public static class PasswordManager
{
	public static string HashPassword(string password)
	{
		string salt = BCrypt.Net.BCrypt.GenerateSalt(4);		
		string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
		return hashedPassword;
	}

	public static bool ValidatePassword(string password, string hashedPassword)
	{
		return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
	}
}
