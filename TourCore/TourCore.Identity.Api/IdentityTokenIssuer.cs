using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TourCore.Identity.Api;

public static class IdentityRegistration
{
    public static IServiceCollection AddIdentityTokenIssuer(this IServiceCollection services, IConfiguration configuration)
    {
        var jwt = configuration.GetSection(JwtIssuerOptions.SectionName).Get<JwtIssuerOptions>() ?? new();
        var user = configuration.GetSection(LocalIdentityOptions.SectionName).Get<LocalIdentityOptions>() ?? new();
        Validate(jwt, user);
        services.Configure<JwtIssuerOptions>(configuration.GetSection(JwtIssuerOptions.SectionName));
        services.Configure<LocalIdentityOptions>(configuration.GetSection(LocalIdentityOptions.SectionName));
        services.AddSingleton<IPasswordVerifier, Pbkdf2PasswordVerifier>();
        services.AddSingleton<IAccessTokenIssuer, JwtAccessTokenIssuer>();
        return services;
    }

    private static void Validate(JwtIssuerOptions jwt, LocalIdentityOptions user)
    {
        if (string.IsNullOrWhiteSpace(jwt.Issuer)) throw new InvalidOperationException("Authentication:Jwt:Issuer is required.");
        if (string.IsNullOrWhiteSpace(jwt.Audience)) throw new InvalidOperationException("Authentication:Jwt:Audience is required.");
        if (string.IsNullOrWhiteSpace(jwt.SigningKey) || jwt.SigningKey.Length < 32) throw new InvalidOperationException("Authentication:Jwt:SigningKey must contain at least 32 characters.");
        if (jwt.AccessTokenMinutes is < 1 or > 1440) throw new InvalidOperationException("Authentication:Jwt:AccessTokenMinutes must be between 1 and 1440.");
        if (string.IsNullOrWhiteSpace(user.Username)) throw new InvalidOperationException("Identity:LocalUser:Username is required.");
        if (string.IsNullOrWhiteSpace(user.PasswordHash) && string.IsNullOrWhiteSpace(user.Password)) throw new InvalidOperationException("Identity:LocalUser:Password or PasswordHash is required.");
        if (user.Roles.Length == 0 || user.Roles.Any(string.IsNullOrWhiteSpace)) throw new InvalidOperationException("Identity:LocalUser:Roles must contain at least one role.");
    }
}

public interface IPasswordVerifier { bool Verify(string password, string encodedHash); }

public sealed class Pbkdf2PasswordVerifier : IPasswordVerifier
{
    public bool Verify(string password, string encodedHash)
    {
        var parts = encodedHash.Split('$');
        if (parts.Length != 4 || parts[0] != "pbkdf2-sha256" || !int.TryParse(parts[1], out var iterations) || iterations < 100_000) return false;
        try
        {
            var salt = Convert.FromBase64String(parts[2]);
            var expected = Convert.FromBase64String(parts[3]);
            var actual = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt, iterations, HashAlgorithmName.SHA256, expected.Length);
            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }
        catch (FormatException) { return false; }
    }
}

public interface IAccessTokenIssuer { TokenResponse Issue(string username, IReadOnlyCollection<string> roles); }

public sealed class JwtAccessTokenIssuer(IOptions<JwtIssuerOptions> options) : IAccessTokenIssuer
{
    private readonly JwtIssuerOptions _options = options.Value;

    public TokenResponse Issue(string username, IReadOnlyCollection<string> roles)
    {
        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(_options.AccessTokenMinutes);
        var claims = new List<Claim> { new(JwtRegisteredClaimNames.Sub, username), new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")) };
        claims.AddRange(roles.Select(role => new Claim("role", role)));
        var token = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, now, expires,
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey)), SecurityAlgorithms.HmacSha256));
        return new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token), "Bearer", checked((int)(expires - now).TotalSeconds), username, roles.ToArray());
    }
}

public sealed record TokenResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("token_type")] string TokenType,
    [property: JsonPropertyName("expires_in")] int ExpiresIn,
    string Username,
    string[] Roles);
