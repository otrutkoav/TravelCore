using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Geography.Entities;

namespace TourCore.Catalog.UnitTests;

[TestClass]
public sealed class CountryTests
{
    private static readonly DateTime CreatedAt = new(2026, 10, 12, 8, 0, 0, DateTimeKind.Utc);

    [TestMethod]
    public void Constructor_normalizes_codes_and_optional_values()
    {
        var country = new Country(
            "  Россия  ",
            "  Russia  ",
            " ru ",
            " ru ",
            " rus ",
            10,
            CreatedAt,
            " 643 ",
            " Россиянин ",
            " Russian ");

        Assert.AreEqual("Россия", country.Name);
        Assert.AreEqual("Russia", country.NameEn);
        Assert.AreEqual("RU", country.Code);
        Assert.AreEqual("RU", country.IsoCode2);
        Assert.AreEqual("RUS", country.IsoCode3);
        Assert.AreEqual("643", country.DigitalCode);
        Assert.AreEqual(CreatedAt, country.CreatedAt);
    }

    [TestMethod]
    public void Constructor_rejects_invalid_iso2_code()
    {
        var exception = Assert.ThrowsException<DomainException>(() =>
            new Country("Россия", "Russia", "RU", "RUS", "RUS", 0, CreatedAt));

        StringAssert.Contains(exception.Message, "ISO2");
    }

    [TestMethod]
    public void Update_changes_values_and_sets_audit_timestamp()
    {
        var country = new Country("Россия", "Russia", "RU", "RU", "RUS", 0, CreatedAt);
        var updatedAt = CreatedAt.AddDays(1);

        country.Update("Беларусь", "Belarus", "BY", "BY", "BLR", 5, updatedAt, "112");

        Assert.AreEqual("Беларусь", country.Name);
        Assert.AreEqual("BY", country.Code);
        Assert.AreEqual(5, country.SortOrder);
        Assert.AreEqual(updatedAt, country.UpdatedAt);
    }
}
