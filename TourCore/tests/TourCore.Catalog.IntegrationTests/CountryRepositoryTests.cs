using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Domain.Geography.Entities;
using TourCore.Infrastructure.Postgres.Persistence;
using TourCore.Infrastructure.Postgres.Persistence.Repositories;

namespace TourCore.Catalog.IntegrationTests;

[TestClass]
[TestCategory("PostgreSQL")]
public sealed class CountryRepositoryTests : PostgresIntegrationTestBase
{
    [TestMethod]
    public async Task Migrations_create_tables_for_all_catalog_groups()
    {
        var connection = DbContext.Database.GetDbConnection();
        await using var command = connection.CreateCommand();
        command.Transaction = DbContext.Database.CurrentTransaction!.GetDbTransaction();
        command.CommandText = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'";

        var actualTables = new HashSet<string>(StringComparer.Ordinal);
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            actualTables.Add(reader.GetString(0));

        foreach (var expectedTable in CatalogModelTests.ExpectedTableNames)
            Assert.IsTrue(actualTables.Contains(expectedTable), $"Table {expectedTable} was not created.");
    }

    [TestMethod]
    public async Task Repository_round_trips_country_through_postgresql()
    {
        var suffix = Guid.NewGuid().ToString("N")[..3].ToUpperInvariant();
        var iso2 = suffix[..2];
        var country = new Country(
            $"Test {suffix}",
            $"Test {suffix}",
            suffix,
            iso2,
            suffix,
            999,
            DateTime.UtcNow);
        var repository = new CountryRepository(DbContext);
        var unitOfWork = new UnitOfWork(DbContext);

        await repository.AddAsync(country, CancellationToken.None);
        await unitOfWork.SaveChangesAsync(CancellationToken.None);
        DbContext.ChangeTracker.Clear();

        var loaded = await repository.GetByIdAsync(country.Id, CancellationToken.None);

        Assert.IsNotNull(loaded);
        Assert.AreEqual(suffix, loaded.Code);
        Assert.IsTrue(await repository.ExistsByIsoCode2Async(iso2, CancellationToken.None));
    }

    [TestMethod]
    public async Task PostgreSQL_enforces_region_country_foreign_key()
    {
        var region = new Region(
            int.MaxValue,
            "Orphan integration region",
            $"X{Guid.NewGuid():N}"[..10],
            DateTime.UtcNow);

        DbContext.Regions.Add(region);

        await Assert.ThrowsExceptionAsync<DbUpdateException>(() =>
            DbContext.SaveChangesAsync(CancellationToken.None));
    }
}
