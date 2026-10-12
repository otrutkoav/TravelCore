namespace TourCore.Admin.Api.Infrastructure;

public sealed class JwtAuthenticationOptions
{
    public const string SectionName = "Authentication:Jwt";
    public string Authority { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string SigningKey { get; set; } = string.Empty;
    public bool RequireHttpsMetadata { get; set; } = true;
}
