namespace Aneejian.Games.ClickMatch.Constants;

public struct AppStrings
{
	public const string ConfigFilePath = "config/config.json";

	public struct GuestUser
	{
		public const string UserName = "guest";
		public const string Password = "Guest";
		public const string DisplayName = "Guest";
		public const string Avatar = "/images/guest-avatar.png";
	}

	public struct NewUser
	{
		public const string UserName = "+++";
		public const string DisplayName = "Add new Profile";
	}

	public struct JSFiles
	{	
		public const string Dexie = "./_content/Aneejian.Games.ClickMatch/js/dexie/dexie.js" ;
		public const string IndexedDb = $"./_content/Aneejian.Games.ClickMatch/js/indexedDb.js?v=1.0.1";
		public const string SessionStorage = "./_content/Aneejian.Games.ClickMatch/js/sessionStorage.js?v=1.0.0";
	}
}
