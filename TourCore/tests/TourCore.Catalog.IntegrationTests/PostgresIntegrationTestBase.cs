using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Npgsql;
using TourCore.Infrastructure.Postgres.Persistence;

namespace TourCore.Catalog.IntegrationTests;

public abstract class PostgresIntegrationTestBase
{
    private const string ConnectionStringVariable = "TOURCORE_TEST_POSTGRES";

    protected TourCoreDbContext DbContext { get; private set; } = null!;
    private IDbContextTransaction _transaction = null!;

    [TestInitialize]
    public async Task InitializeDatabaseAsync()
    {
        var connectionString = Environment.GetEnvironmentVariable(ConnectionStringVariable);
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Assert.Inconclusive(
                $"Set {ConnectionStringVariable} to a dedicated PostgreSQL test database connection string.");
        }

        var connectionBuilder = new NpgsqlConnectionStringBuilder(connectionString!);
        var databaseName = connectionBuilder.Database ?? string.Empty;
        if (!databaseName.Contains("test", StringComparison.OrdinalIgnoreCase))
        {
            Assert.Fail(
                $"Integration tests refuse database '{databaseName}'. " +
                "The database name must contain 'test'.");
        }

        var options = new DbContextOptionsBuilder<TourCoreDbContext>()
            .UseNpgsql(connectionBuilder.ConnectionString)
            .Options;

        DbContext = new TourCoreDbContext(options);
        await DbContext.Database.MigrateAsync();
        _transaction = await DbContext.Database.BeginTransactionAsync();
    }

    [TestCleanup]
    public async Task RollbackAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        if (DbContext is not null)
            await DbContext.DisposeAsync();
    }
}
