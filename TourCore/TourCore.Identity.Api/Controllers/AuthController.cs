using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace TourCore.Identity.Api.Controllers;

[ApiController]
[AllowAnonymous]
public sealed class AuthController(IOptions<LocalIdentityOptions> userOptions, IPasswordVerifier passwordVerifier, IAccessTokenIssuer tokenIssuer, ILogger<AuthController> logger) : ControllerBase
{
    private readonly LocalIdentityOptions _user = userOptions.Value;

    [HttpPost("api/v1/auth/login")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<TokenResponse> Login([FromBody] LoginRequest request) => Authenticate(request.Username, request.Password);

    [HttpPost("connect/token")]
    [Consumes("application/x-www-form-urlencoded")]
    [ProducesResponseType<TokenResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<TokenResponse> Token([FromForm] OAuthTokenRequest request)
    {
        if (!string.Equals(request.GrantType, "password", StringComparison.Ordinal)) return BadRequest(new OAuthError("unsupported_grant_type"));
        return Authenticate(request.Username, request.Password, oauthError: true);
    }

    private ActionResult<TokenResponse> Authenticate(string username, string password, bool oauthError = false)
    {
        var supplied = Encoding.UTF8.GetBytes(username ?? string.Empty);
        var configured = Encoding.UTF8.GetBytes(_user.Username);
        var usernameMatches = supplied.Length == configured.Length && CryptographicOperations.FixedTimeEquals(supplied, configured);
        var passwordMatches = string.IsNullOrEmpty(_user.PasswordHash)
            ? FixedTimeEquals(password ?? string.Empty, _user.Password)
            : passwordVerifier.Verify(password ?? string.Empty, _user.PasswordHash);
        if (!usernameMatches || !passwordMatches)
        {
            logger.LogWarning("Failed login attempt for {Username}", username);
            if (oauthError) return BadRequest(new OAuthError("invalid_grant"));
            return Unauthorized();
        }
        logger.LogInformation("Access token issued for {Username} with roles {Roles}", _user.Username, _user.Roles);
        return Ok(tokenIssuer.Issue(_user.Username, _user.Roles));
    }

    private static bool FixedTimeEquals(string suppliedValue, string configuredValue)
    {
        var supplied = Encoding.UTF8.GetBytes(suppliedValue);
        var configured = Encoding.UTF8.GetBytes(configuredValue);
        return supplied.Length == configured.Length && CryptographicOperations.FixedTimeEquals(supplied, configured);
    }
}

public sealed record LoginRequest(string Username, string Password);
public sealed class OAuthTokenRequest
{
    [FromForm(Name = "grant_type")] public string GrantType { get; init; } = string.Empty;
    [FromForm(Name = "username")] public string Username { get; init; } = string.Empty;
    [FromForm(Name = "password")] public string Password { get; init; } = string.Empty;
}
public sealed record OAuthError(string Error);
