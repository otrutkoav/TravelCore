using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace TourCore.Admin.Api.Infrastructure;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddTourCoreAuthorization(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = configuration
            .GetSection(JwtAuthenticationOptions.SectionName)
            .Get<JwtAuthenticationOptions>() ?? new JwtAuthenticationOptions();

        Validate(settings);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.RequireHttpsMetadata = settings.RequireHttpsMetadata;

                if (!string.IsNullOrWhiteSpace(settings.Authority))
                {
                    options.Authority = settings.Authority.TrimEnd('/');
                    options.Audience = settings.Audience;
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
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(settings.SigningKey)),
                    ValidateIssuer = true,
                    ValidIssuer = settings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = settings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    NameClaimType = JwtRegisteredClaimNames.Sub,
                    RoleClaimType = "role"
                };
            });

        services.AddAuthorizationBuilder()
            .SetFallbackPolicy(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build())
            .AddPolicy(AuthorizationPolicies.CatalogRead, policy => policy.RequireRole(
                AuthorizationRoles.CatalogReader,
                AuthorizationRoles.CatalogEditor,
                AuthorizationRoles.CatalogAdmin))
            .AddPolicy(AuthorizationPolicies.CatalogWrite, policy => policy.RequireRole(
                AuthorizationRoles.CatalogEditor,
                AuthorizationRoles.CatalogAdmin))
            .AddPolicy(AuthorizationPolicies.CatalogDelete, policy => policy.RequireRole(
                AuthorizationRoles.CatalogAdmin))
            .AddPolicy(AuthorizationPolicies.CatalogFinanceWrite, policy => policy.RequireRole(
                AuthorizationRoles.CatalogEditor,
                AuthorizationRoles.CatalogAdmin));

        services.Configure<Microsoft.AspNetCore.Mvc.MvcOptions>(options =>
            options.Conventions.Add(new CatalogAuthorizationConvention()));

        return services;
    }

    public static void AddTourCoreJwtSecurity(
        this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions options,
        IConfiguration configuration)
    {
        var tokenUrl = configuration["Authentication:Swagger:TokenUrl"];
        if (Uri.TryCreate(tokenUrl, UriKind.Absolute, out var tokenEndpoint))
        {
            var oauthScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Description = "Вход через TourCore Identity по логину и паролю.",
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = tokenEndpoint,
                        Scopes = new Dictionary<string, string>()
                    }
                },
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "TourCoreIdentity"
                }
            };
            options.AddSecurityDefinition("TourCoreIdentity", oauthScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                [oauthScheme] = Array.Empty<string>()
            });
            return;
        }

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

    private static void Validate(JwtAuthenticationOptions settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Audience))
            throw new InvalidOperationException("Authentication:Jwt:Audience is required.");

        if (!string.IsNullOrWhiteSpace(settings.Authority))
            return;

        if (string.IsNullOrWhiteSpace(settings.Issuer))
            throw new InvalidOperationException("Authentication:Jwt:Issuer is required for symmetric JWT validation.");
        if (string.IsNullOrWhiteSpace(settings.SigningKey) || settings.SigningKey.Length < 32)
            throw new InvalidOperationException("Authentication:Jwt:SigningKey must contain at least 32 characters.");
    }

    private sealed class CatalogAuthorizationConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                // Product endpoints are currently protected by the authenticated fallback policy.
                if (controller.ControllerName.StartsWith("Tour", StringComparison.Ordinal))
                    continue;

                controller.Filters.Add(new AuthorizeFilter(AuthorizationPolicies.CatalogRead));
                var finance = controller.ControllerType.Namespace?.Contains(
                    ".Controllers.Finance", StringComparison.Ordinal) == true;

                foreach (var action in controller.Actions)
                {
                    var methods = action.ActionMethod
                        .GetCustomAttributes(true)
                        .OfType<Microsoft.AspNetCore.Mvc.Routing.HttpMethodAttribute>()
                        .SelectMany(attribute => attribute.HttpMethods)
                        .ToHashSet(StringComparer.OrdinalIgnoreCase);

                    if (methods.Contains("DELETE"))
                        action.Filters.Add(new AuthorizeFilter(AuthorizationPolicies.CatalogDelete));
                    else if (methods.Overlaps(new[] { "POST", "PUT", "PATCH" }))
                        action.Filters.Add(new AuthorizeFilter(
                            finance
                                ? AuthorizationPolicies.CatalogFinanceWrite
                                : AuthorizationPolicies.CatalogWrite));
                }
            }
        }
    }
}
