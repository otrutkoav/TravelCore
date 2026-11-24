using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Infrastructure.Postgres.Persistence;

namespace TourCore.Pricing.Infrastructure.Postgres;

public static class DependencyInjection
{
    public static IServiceCollection AddPricingPostgresInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException("Pricing PostgreSQL connection string is required.", nameof(connectionString));

        services.AddDbContext<PricingDbContext>(options =>
            options.UseNpgsql(connectionString, npgsql =>
                npgsql.MigrationsAssembly(typeof(PricingDbContext).Assembly.FullName)));
        services.AddScoped<IPricePackageRepository, PricePackageRepository>();
        services.AddScoped<IPriceBlockRepository, PriceBlockRepository>();
        services.AddScoped<ISPOTypeRepository, SPOTypeRepository>();
        services.AddScoped<ISeasonRepository, SeasonRepository>();
        return services;
    }
}
