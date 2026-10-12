using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Domain.Avia.Entities;
using TourCore.Domain.Bus.Entities;
using TourCore.Domain.Finance.Entities;
using TourCore.Domain.Geography.Entities;
using TourCore.Domain.Hotels.Entities;
using TourCore.Domain.Railway.Entities;
using TourCore.Domain.Seating.Entities;
using TourCore.Domain.Services.Entities;
using TourCore.Domain.Transfers.Entities;
using TourCore.Domain.Transportation.Entities;
using TourCore.Infrastructure.Postgres.Persistence;

namespace TourCore.Catalog.IntegrationTests;

[TestClass]
[TestCategory("EFModel")]
public sealed class CatalogModelTests
{
    private static readonly IReadOnlyDictionary<Type, string> CatalogTables =
        new Dictionary<Type, string>
        {
            [typeof(Country)] = "Countries",
            [typeof(Region)] = "Regions",
            [typeof(City)] = "Cities",
            [typeof(Resort)] = "Resorts",
            [typeof(Hotel)] = "Hotels",
            [typeof(HotelCategory)] = "HotelCategories",
            [typeof(AccommodationType)] = "AccommodationTypes",
            [typeof(AccommodationPlacementRule)] = "AccommodationPlacementRules",
            [typeof(AccommodationPlacementRuleAgeRange)] = "AccommodationPlacementRuleAgeRanges",
            [typeof(RoomType)] = "RoomTypes",
            [typeof(RoomCategory)] = "RoomCategories",
            [typeof(MealType)] = "MealTypes",
            [typeof(Rate)] = "Rates",
            [typeof(RealCourse)] = "RealCourses",
            [typeof(AirClass)] = "AirClasses",
            [typeof(Aircraft)] = "Aircraft",
            [typeof(Airline)] = "Airlines",
            [typeof(Airport)] = "Airports",
            [typeof(Charter)] = "Charters",
            [typeof(CharterSeason)] = "CharterSeasons",
            [typeof(BusTransfer)] = "BusTransfers",
            [typeof(BusTransferPoint)] = "BusTransferPoints",
            [typeof(BusSchedule)] = "BusSchedules",
            [typeof(RailwayTransfer)] = "RailwayTransfers",
            [typeof(TrainSchedule)] = "TrainSchedules",
            [typeof(Transport)] = "Transports",
            [typeof(Transfer)] = "Transfers",
            [typeof(TransferDirection)] = "TransferDirections",
            [typeof(VehiclePlan)] = "VehiclePlans",
            [typeof(SeatingCell)] = "SeatingCells",
            [typeof(ServiceType)] = "ServiceTypes"
        };

    [TestMethod]
    public void Every_active_catalog_entity_has_table_and_primary_key_mapping()
    {
        using var context = CreateContext();

        foreach (var expected in CatalogTables)
        {
            var entityType = context.Model.FindEntityType(expected.Key);
            Assert.IsNotNull(entityType, $"{expected.Key.Name} is not part of the EF model.");
            Assert.AreEqual(expected.Value, entityType.GetTableName(), expected.Key.Name);
            Assert.IsNotNull(entityType.FindPrimaryKey(), $"{expected.Key.Name} has no primary key.");
        }
    }

    [TestMethod]
    public void Catalog_table_inventory_stays_in_sync_with_db_context()
    {
        var dbSetEntityTypes = typeof(TourCoreDbContext)
            .GetProperties()
            .Where(property =>
                property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
            .Select(property => property.PropertyType.GetGenericArguments()[0])
            .Where(type => type.Namespace?.StartsWith("TourCore.Domain.Tours", StringComparison.Ordinal) != true)
            .ToHashSet();

        CollectionAssert.AreEquivalent(
            CatalogTables.Keys.ToArray(),
            dbSetEntityTypes.ToArray(),
            "Update CatalogModelTests when a Catalog DbSet is added or removed.");
    }

    [TestMethod]
    public void Catalog_relations_are_configured_as_foreign_keys()
    {
        using var context = CreateContext();

        AssertForeignKey<Region>(context.Model, "CountryId");
        AssertForeignKey<City>(context.Model, "CountryId");
        AssertForeignKey<City>(context.Model, "RegionId");
        AssertForeignKey<Resort>(context.Model, "CountryId");
        AssertForeignKey<Hotel>(context.Model, "CountryId");
        AssertForeignKey<Hotel>(context.Model, "CityId");
        AssertForeignKey<Hotel>(context.Model, "ResortId");
        AssertForeignKey<Hotel>(context.Model, "CategoryId");
        AssertForeignKey<Airport>(context.Model, "CityId");
        AssertForeignKey<Charter>(context.Model, "DepartureCityId");
        AssertForeignKey<Charter>(context.Model, "ArrivalCityId");
        AssertForeignKey<BusTransfer>(context.Model, "CountryFromId");
        AssertForeignKey<BusTransfer>(context.Model, "CityFromId");
        AssertForeignKey<BusTransfer>(context.Model, "CountryToId");
        AssertForeignKey<BusTransfer>(context.Model, "CityToId");
        AssertForeignKey<RailwayTransfer>(context.Model, "CountryFromId");
        AssertForeignKey<RailwayTransfer>(context.Model, "CityFromId");
        AssertForeignKey<RailwayTransfer>(context.Model, "CountryToId");
        AssertForeignKey<RailwayTransfer>(context.Model, "CityToId");
        AssertForeignKey<Transfer>(context.Model, "CityId");
        AssertForeignKey<Transfer>(context.Model, "DirectionId");
        AssertForeignKey<VehiclePlan>(context.Model, "TransportId");
        AssertForeignKey<SeatingCell>(context.Model, "VehiclePlanId");
        AssertForeignKey<AccommodationPlacementRuleAgeRange>(
            context.Model, "AccommodationPlacementRuleId");
    }

    internal static IReadOnlyCollection<string> ExpectedTableNames => CatalogTables.Values.ToArray();

    private static TourCoreDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<TourCoreDbContext>()
            .UseNpgsql("Host=localhost;Database=model_only;Username=model_only")
            .Options;
        return new TourCoreDbContext(options);
    }

    private static void AssertForeignKey<TEntity>(IModel model, string propertyName)
    {
        var entityType = model.FindEntityType(typeof(TEntity));
        Assert.IsNotNull(entityType, typeof(TEntity).Name);
        Assert.IsTrue(
            entityType.GetForeignKeys().Any(foreignKey =>
                foreignKey.Properties.Any(property => property.Name == propertyName)),
            $"{typeof(TEntity).Name}.{propertyName} is not configured as a foreign key.");
    }
}
