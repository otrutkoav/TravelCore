using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace TourCore.Pricing.Api.Infrastructure;

public static class PricingAuthorizationPolicies
{
    public const string Read = "Pricing.Read";
    public const string Write = "Pricing.Write";
    public const string Delete = "Pricing.Delete";
}

internal static class PricingAuthorization
{
    public static IServiceCollection AddPricingAuthorization(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection("Authentication:Jwt");
        var authority = section["Authority"];
        var issuer = section["Issuer"];
        var audience = section["Audience"];
        var signingKey = section["SigningKey"];
        var requireHttpsMetadata = section.GetValue("RequireHttpsMetadata", true);

        if (string.IsNullOrWhiteSpace(audience))
            throw new InvalidOperationException("Authentication:Jwt:Audience is required.");
        if (string.IsNullOrWhiteSpace(authority) && (string.IsNullOrWhiteSpace(issuer) || signingKey?.Length < 32))
            throw new InvalidOperationException("Configure JWT Authority or Issuer and a SigningKey of at least 32 characters.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.RequireHttpsMetadata = requireHttpsMetadata;
                if (!string.IsNullOrWhiteSpace(authority))
                {
                    options.Authority = authority.TrimEnd('/');
                    options.Audience = audience;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtRegisteredClaimNames.Sub,
                        RoleClaimType = "role"
                    };
                    return;
                }

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey!)),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    NameClaimType = JwtRegisteredClaimNames.Sub,
                    RoleClaimType = "role"
                };
            });

        services.AddAuthorizationBuilder()
            .AddPolicy(PricingAuthorizationPolicies.Read, policy =>
                policy.RequireRole("PricingReader", "PricingEditor", "PricingAdmin"))
            .AddPolicy(PricingAuthorizationPolicies.Write, policy =>
                policy.RequireRole("PricingEditor", "PricingAdmin"))
            .AddPolicy(PricingAuthorizationPolicies.Delete, policy =>
                policy.RequireRole("PricingAdmin"));
        return services;
    }

    public static void AddPricingJwtSecurity(
        this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions options,
        IConfiguration configuration)
    {
        var scheme = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "JWT access token: Bearer {token}",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme
            }
        };
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, scheme);
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            [scheme] = Array.Empty<string>()
        });
    }
}
