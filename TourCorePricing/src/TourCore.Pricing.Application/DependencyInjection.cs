using Microsoft.Extensions.DependencyInjection;
using TourCore.Pricing.Application.PricePackages;
using TourCore.Pricing.Application.PriceBlocks;
using TourCore.Pricing.Application.SPOTypes;
using TourCore.Pricing.Application.Seasons;
using TourCore.Pricing.Application.HotelPrices;
using TourCore.Pricing.Application.FlightPrices;
using TourCore.Pricing.Application.BusPrices;
using TourCore.Pricing.Application.RailwayPrices;

namespace TourCore.Pricing.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPricingApplication(this IServiceCollection services)
    {
        services.AddScoped<PricePackageService>();
        services.AddScoped<PriceBlockService>();
        services.AddScoped<SPOTypeService>();
        services.AddScoped<SeasonService>();
        services.AddScoped<HotelPriceService>();
        services.AddScoped<FlightPriceService>();
        services.AddScoped<BusTransferPriceService>();
        services.AddScoped<RailwayTransferPriceService>();
        return services;
    }
}
