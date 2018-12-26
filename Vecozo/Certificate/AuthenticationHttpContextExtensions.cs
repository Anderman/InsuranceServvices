using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Vecozo.Certificate
{
	public static class AuthenticationHttpContextExtensions
	{
		public static void AddNameClaim(this HttpContext httpContext, string name)
		{
			var identity = httpContext.User.Identity;
			var claimsIdentity = new ClaimsIdentity(identity);
			claimsIdentity.AddClaim(new Claim("name", name));
			httpContext.User.AddIdentity(claimsIdentity);
		}
	}
}