using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using TourCore.Infrastructure.Postgres.Persistence;

namespace TourCore.Admin.Api.Infrastructure;

public sealed class PostgresReadinessHealthCheck : IHealthCheck
{
    private readonly TourCoreDbContext _dbContext;
    public PostgresReadinessHealthCheck(TourCoreDbContext dbContext) => _dbContext = dbContext;

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (!await _dbContext.Database.CanConnectAsync(cancellationToken))
                return HealthCheckResult.Unhealthy("PostgreSQL connection failed.");

            var pending = (await _dbContext.Database
                    .GetPendingMigrationsAsync(cancellationToken))
                .Take(5)
                .ToArray();

            return pending.Length == 0
                ? HealthCheckResult.Healthy("PostgreSQL is available and migrations are current.")
                : HealthCheckResult.Unhealthy(
                    $"Pending database migrations: {string.Join(", ", pending)}");
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy(
                "PostgreSQL readiness check failed.", exception);
        }
    }
}
