using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Pricing.Domain.PriceBlocks;
using TourCore.Pricing.Domain.SPOTypes;

namespace TourCore.Pricing.Tests;

[TestClass]
public sealed class SPOTypeTests
{
    [TestMethod]
    public void Constructor_normalizes_names()
    {
        var createdAt = new DateTime(2026, 11, 23, 18, 0, 0, DateTimeKind.Utc);
        var spoType = new SPOType(" Ordinary ", " базовые ", "admin", createdAt);

        Assert.AreEqual(0, spoType.Id);
        Assert.AreEqual("Ordinary", spoType.Name);
        Assert.AreEqual("базовые", spoType.NameLat);
        Assert.AreEqual("admin", spoType.CreatedByUserId);
        Assert.AreEqual(createdAt, spoType.CreatedAt);
    }

    [TestMethod]
    public void Cost_offer_state_values_are_stable()
    {
        Assert.AreEqual((byte)0, (byte)CostOfferState.New);
        Assert.AreEqual((byte)1, (byte)CostOfferState.InChecked);
        Assert.AreEqual((byte)2, (byte)CostOfferState.ActivatedForNetto);
        Assert.AreEqual((byte)3, (byte)CostOfferState.Activated);
        Assert.AreEqual((byte)4, (byte)CostOfferState.Deactivated);
    }
}
