using System.IdentityModel.Tokens.Jwt;
using TourCore.Pricing.Application.Common;
using TourCore.Pricing.Application.Abstractions;

namespace TourCore.Pricing.Api.Infrastructure;

internal static class PricingApiServices
{
    public static IServiceCollection AddPricingApiServices(this IServiceCollection services)
    {
        services.AddSingleton<IClock, SystemClock>();
        services.AddScoped<ICurrentUser, HttpCurrentUser>();
        return services;
    }
}

internal sealed class SystemClock : IClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}

internal sealed class HttpCurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpCurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrWhiteSpace(userId))
                throw new ValidationException("Authenticated user identifier is missing.");
            return userId;
        }
    }
}
