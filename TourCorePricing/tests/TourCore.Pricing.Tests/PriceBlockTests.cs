using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Pricing.Domain.PriceBlocks;

namespace TourCore.Pricing.Tests;

[TestClass]
public sealed class PriceBlockTests
{
    [TestMethod]
    public void New_block_belongs_to_one_package_and_starts_new()
    {
        var createdAt = new DateTime(2026, 11, 23, 16, 0, 0, DateTimeKind.Utc);
        var block = CreateBlock(createdAt);

        Assert.AreEqual(10, block.PricePackageId);
        Assert.AreEqual(20, block.SPOTypeId);
        Assert.AreEqual(3, block.ServiceTypeId);
        Assert.AreEqual(CostOfferState.New, block.State);
        Assert.AreEqual("user", block.CreatedByUserId);
    }

    [TestMethod]
    public void State_change_records_actor_and_timestamp()
    {
        var block = CreateBlock(DateTime.UtcNow);
        var changedAt = new DateTime(2026, 11, 23, 17, 0, 0, DateTimeKind.Utc);

        block.ChangeState(CostOfferState.Activated, "manager", changedAt);

        Assert.AreEqual(CostOfferState.Activated, block.State);
        Assert.AreEqual("manager", block.ActivatedByUserId);
        Assert.AreEqual(changedAt, block.ActivatedAt);
    }

    private static PriceBlock CreateBlock(DateTime createdAt)
    {
        return new PriceBlock(
            10, 20, 2026, 3, 100, 1, "Summer block", "S-1", "Description",
            null, null, null, null, null, null, false, "user", createdAt);
    }
}
