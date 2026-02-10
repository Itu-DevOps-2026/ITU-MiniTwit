using System.Text.Encodings.Web;

namespace MiniTwit.Web.Authentication;

using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;


public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
        }

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]!);
            if (!authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid scheme"));
            }

            var credentialsBytes = Convert.FromBase64String(authHeader.Parameter!);
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(':', 2);
            var username = credentials[0];
            var password = credentials[1];
            
            if (username != "simulator" && password != "super_safe!")
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "invalid") };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            var validClaims = new[] { new Claim(ClaimTypes.Name, username)};
            var validIdentity = new ClaimsIdentity(validClaims, Scheme.Name);
            var validPrincipal = new ClaimsPrincipal(validIdentity);
            var validTicket = new AuthenticationTicket(validPrincipal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(validTicket));
        }
        catch
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
        }
    }
}