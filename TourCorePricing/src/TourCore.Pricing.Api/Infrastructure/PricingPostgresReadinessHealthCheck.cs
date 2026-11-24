using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TourCore.Pricing.Infrastructure.Postgres.Persistence;

namespace TourCore.Pricing.Api.Infrastructure;

internal sealed class PricingPostgresReadinessHealthCheck : IHealthCheck
{
    private readonly PricingDbContext _dbContext;

    public PricingPostgresReadinessHealthCheck(PricingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!await _dbContext.Database.CanConnectAsync(cancellationToken))
                return HealthCheckResult.Unhealthy("Pricing PostgreSQL connection failed.");

            var pendingMigrations = (await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken))
                .Take(5)
                .ToArray();
            return pendingMigrations.Length == 0
                ? HealthCheckResult.Healthy("Pricing PostgreSQL is available and migrations are current.")
                : HealthCheckResult.Unhealthy($"Pending database migrations: {string.Join(", ", pendingMigrations)}");
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy("Pricing PostgreSQL readiness check failed.", exception);
        }
    }
}
