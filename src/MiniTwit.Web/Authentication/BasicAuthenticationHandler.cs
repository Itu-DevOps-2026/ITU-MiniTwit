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
            // Still return success but with no simulator claim
            return Task.FromResult(CreateTicket(isSimulator: false));
        }

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]!);

            if (!authHeader.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
            {
                return Task.FromResult(CreateTicket(isSimulator: false));
            }

            var credentialsBytes = Convert.FromBase64String(authHeader.Parameter!);
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(':', 2);

            if (credentials.Length != 2)
                return Task.FromResult(CreateTicket(isSimulator: false));

            var username = credentials[0];
            var password = credentials[1];

            var isValid = username == "simulator" && password == "super_safe!";

            return Task.FromResult(CreateTicket(isValid));
        }
        catch
        {
            return Task.FromResult(CreateTicket(isSimulator: false));
        }
    }

    private AuthenticateResult CreateTicket(bool isSimulator)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "basic-user")
        };

        if (isSimulator)
        {
            claims.Add(new Claim("Simulator", "true"));
        }

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}