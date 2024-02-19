using System.Text;
using System.Security.Cryptography;

namespace Aneejian.Games.ClickMatch.Security
{
	public static class UrlSafeEncryption
	{
		public static string Encrypt(string input)
		{
			
			byte[] data = Encoding.UTF8.GetBytes(input);
			string base64 = Convert.ToBase64String(data);
			string urlSafe = base64.Replace('+', '-').Replace('/', '_');
			return urlSafe;
		}

		public static string Decrypt(string input)
		{
			string base64 = input.Replace('-', '+').Replace('_', '/');
			byte[] data = Convert.FromBase64String(base64);
			string result = Encoding.UTF8.GetString(data);
			return result;
		}
	}
}
