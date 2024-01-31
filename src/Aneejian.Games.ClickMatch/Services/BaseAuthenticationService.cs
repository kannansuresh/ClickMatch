namespace Aneejian.Games.ClickMatch.Services;

public class BaseAuthenticationService
{
	public event Action? OnChange;
	private bool _loggedIn;

	public bool IsLoggedIn()
	{
		return _loggedIn;
	}

	public void Login()
	{
		_loggedIn = true;
		NotifyStateChanged();
	}

	public void Logout()
	{
		_loggedIn = false;
		NotifyStateChanged();
	}

	private void NotifyStateChanged() => OnChange?.Invoke();
}

