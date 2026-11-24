using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Pricing.Domain.Common;
using TourCore.Pricing.Domain.HotelPrices;

namespace TourCore.Pricing.Tests;

[TestClass]
public sealed class HotelPriceTests
{
    private static readonly DateTime Now = new(2026, 11, 24, 10, 0, 0, DateTimeKind.Utc);

    [TestMethod]
    public void Fixed_accommodation_requires_accommodation_type()
    {
        Assert.ThrowsException<DomainException>(() => CreateRule(HotelPriceModel.FixedAccommodation, null, 100m));
    }

    [TestMethod]
    public void Base_occupancy_rule_keeps_base_amount_and_audit()
    {
        var rule = CreateRule(HotelPriceModel.BaseOccupancy, 5, 100m);

        Assert.AreEqual(100m, rule.NettoAmount);
        Assert.AreEqual("user", rule.CreatedByUserId);
        Assert.AreEqual(Now, rule.CreatedAt);
    }

    [TestMethod]
    public void Percentage_component_requires_percentage_base_and_no_amount()
    {
        Assert.ThrowsException<DomainException>(() => new HotelPriceComponent(
            1, HotelPriceComponentType.GuestDiscount, PriceGuestType.Child,
            PricePlacementType.Any, 2, 11, PriceOperation.SubtractPercent,
            PriceApplicationUnit.PerPersonPerNight, 10m, null, 20m,
            PercentageBase.AdultMainPlacePrice, "user", Now));
    }

    [TestMethod]
    public void Meal_upgrade_cannot_point_to_the_same_meal_type()
    {
        Assert.ThrowsException<DomainException>(() => new HotelMealPriceRule(
            1, 2, null, null, null, 3, 3,
            new DateOnly(2027, 1, 1), new DateOnly(2027, 12, 31), null, null,
            (byte)PriceDaysOfWeek.All, null, PricePlacementType.Any, null, null,
            PriceApplicationUnit.PerPersonPerNight, PriceOperation.AddAmount,
            10m, null, null, null, null, 0, "user", Now));
    }

    private static HotelPriceRule CreateRule(HotelPriceModel model, int? accommodationTypeId, decimal? netAmount) =>
        new(1, 2, 3, 4, accommodationTypeId, null, model, 0,
            new DateOnly(2027, 1, 1), new DateOnly(2027, 12, 31), null, null,
            (byte)PriceDaysOfWeek.All, null, null, AgeCalculationDate.CheckIn,
            PriceApplicationUnit.PerRoomPerNight, netAmount, null, null, "user", Now);
}
