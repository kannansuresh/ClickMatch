using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aneejian.Games.ClickMatch.Services;

public class AuthStateProvider(AuthenticationService authenticationService) : AuthenticationStateProvider
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
}
