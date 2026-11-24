using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Pricing.Domain.Seasons;

namespace TourCore.Pricing.Tests;

[TestClass]
public sealed class SeasonTests
{
    [TestMethod]
    public void Constructor_sets_values_and_audit()
    {
        var createdAt = new DateTime(2026, 11, 23, 18, 0, 0, DateTimeKind.Utc);
        var season = new Season(" Зима ", " Winter ", true, false, "admin", createdAt);

        Assert.AreEqual("Зима", season.Name);
        Assert.AreEqual("Winter", season.NameLat);
        Assert.IsTrue(season.IsActive);
        Assert.IsFalse(season.IsMain);
        Assert.AreEqual("admin", season.CreatedByUserId);
        Assert.AreEqual(createdAt, season.CreatedAt);
        Assert.IsNull(season.UpdatedAt);
    }
}
