namespace TourCore.Identity.Api;

public sealed class JwtIssuerOptions
{
    public const string SectionName = "Authentication:Jwt";
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SigningKey { get; set; } = string.Empty;
    public int AccessTokenMinutes { get; set; } = 60;
}

public sealed class LocalIdentityOptions
{
    public const string SectionName = "Identity:LocalUser";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string[] Roles { get; set; } = [];
}
