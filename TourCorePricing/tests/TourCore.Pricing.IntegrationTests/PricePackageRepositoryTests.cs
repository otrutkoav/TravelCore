using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using TourCore.Pricing.Application.Abstractions;
using TourCore.Pricing.Domain.PricePackages;
using TourCore.Pricing.Infrastructure.Postgres;
using TourCore.Pricing.Infrastructure.Postgres.Persistence;

namespace TourCore.Pricing.IntegrationTests;

[TestClass]
public sealed class PricePackageRepositoryTests
{
    private const string ConnectionStringVariable = "PRICING_TEST_POSTGRES";
    private ServiceProvider? _serviceProvider;
    private PricingDbContext? _dbContext;

    [TestInitialize]
    public async Task InitializeAsync()
    {
        var connectionString = Environment.GetEnvironmentVariable(ConnectionStringVariable);
        if (string.IsNullOrWhiteSpace(connectionString))
            Assert.Inconclusive($"Set {ConnectionStringVariable} to a dedicated PostgreSQL test database.");

        var connectionBuilder = new NpgsqlConnectionStringBuilder(connectionString);
        if (!(connectionBuilder.Database ?? string.Empty).Contains("test", StringComparison.OrdinalIgnoreCase))
            Assert.Fail("Integration tests require a database whose name contains 'test'.");

        var services = new ServiceCollection();
        services.AddPricingPostgresInfrastructure(connectionBuilder.ConnectionString);
        _serviceProvider = services.BuildServiceProvider();
        _dbContext = _serviceProvider.GetRequiredService<PricingDbContext>();
        await _dbContext.Database.MigrateAsync();
    }

    [TestCleanup]
    public async Task CleanupAsync()
    {
        if (_dbContext is not null)
        {
            _dbContext.PricePackages.RemoveRange(_dbContext.PricePackages);
            await _dbContext.SaveChangesAsync();
        }

        if (_serviceProvider is not null)
            await _serviceProvider.DisposeAsync();
    }

    [TestMethod]
    public async Task Repository_excludes_soft_deleted_packages()
    {
        var repository = _serviceProvider!.GetRequiredService<IPricePackageRepository>();
        var active = new PricePackage("Active", 1, false, "test-user", DateTime.UtcNow);
        var deleted = new PricePackage("Deleted", 1, false, "test-user", DateTime.UtcNow);
        deleted.Delete(DateTime.UtcNow);
        await repository.AddAsync(active, CancellationToken.None);
        await repository.AddAsync(deleted, CancellationToken.None);
        await repository.SaveChangesAsync(CancellationToken.None);

        var result = await repository.ListAsync(null, null, null, CancellationToken.None);

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Active", result[0].Name);
    }
}
