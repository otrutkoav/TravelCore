using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Pricing.Domain.Common;
using TourCore.Pricing.Domain.PricePackages;

namespace TourCore.Pricing.Tests;

[TestClass]
public sealed class PricePackageTests
{
    [TestMethod]
    public void Constructor_normalizes_and_records_package_data()
    {
        var createdAt = new DateTime(2026, 11, 23, 12, 0, 0, DateTimeKind.Utc);

        var pricePackage = new PricePackage("  Turkey summer  ", 7, false, "admin", createdAt);

        Assert.AreEqual("Turkey summer", pricePackage.Name);
        Assert.AreEqual(7, pricePackage.CountryId);
        Assert.AreEqual("admin", pricePackage.CreatedByUserId);
        Assert.AreEqual(createdAt, pricePackage.CreatedAt);
        Assert.IsFalse(pricePackage.Deleted);
    }

    [TestMethod]
    public void Deleted_package_cannot_be_updated()
    {
        var pricePackage = new PricePackage("Turkey", 7, false, "admin", DateTime.UtcNow);
        pricePackage.Delete(DateTime.UtcNow);

        Assert.ThrowsExactly<DomainException>(() =>
            pricePackage.Update("Turkey 2027", 7, false, DateTime.UtcNow));
    }
}
