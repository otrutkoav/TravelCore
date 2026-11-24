using Microsoft.Extensions.DependencyInjection;
using TourCore.Pricing.Application.PricePackages;
using TourCore.Pricing.Application.PriceBlocks;
using TourCore.Pricing.Application.SPOTypes;
using TourCore.Pricing.Application.Seasons;

namespace TourCore.Pricing.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPricingApplication(this IServiceCollection services)
    {
        services.AddScoped<PricePackageService>();
        services.AddScoped<PriceBlockService>();
        services.AddScoped<SPOTypeService>();
        services.AddScoped<SeasonService>();
        return services;
    }
}
