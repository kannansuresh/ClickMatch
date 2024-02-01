using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Aneejian.Games.ClickMatch.Services.Authentication;

public class AuthStateProvider(AuthenticationService authenticationService) : AuthenticationStateProvider, INotifyAuthenticationStateChanged
{
	private readonly AuthenticationService _authenticationService = authenticationService;

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		ClaimsIdentity identity;

		if (_authenticationService.IsAuthenticated)
		{
			identity = new ClaimsIdentity(new[] { new Claim("name", "username") }, "apiauth_type");
		}
		else
		{
			identity = new ClaimsIdentity();
		}

		var user = new ClaimsPrincipal(identity);

		return await Task.FromResult(new AuthenticationState(user));
	}

	public void OnAuthenticationStateChanged()
	{
		NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
	}
}