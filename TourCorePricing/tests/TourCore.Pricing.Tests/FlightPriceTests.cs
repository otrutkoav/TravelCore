using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Pricing.Domain.Common;
using TourCore.Pricing.Domain.FlightPrices;
using TourCore.Pricing.Domain.HotelPrices;

namespace TourCore.Pricing.Tests;

[TestClass]
public sealed class FlightPriceTests
{
    private static readonly DateTime Now = new(2026, 11, 24, 12, 0, 0, DateTimeKind.Utc);

    [TestMethod]
    public void Price_describes_one_passenger_on_one_charter_segment()
    {
        var price = Create(ageFrom: 2, ageTo: 11, netto: 150m, brutto: 175m);

        Assert.AreEqual(10, price.CharterId);
        Assert.AreEqual(20, price.AirClassId);
        Assert.AreEqual((short?)2, price.AgeFrom);
        Assert.AreEqual((short?)11, price.AgeTo);
        Assert.AreEqual(150m, price.NettoAmount);
        Assert.AreEqual(175m, price.BruttoAmount);
    }

    [TestMethod]
    public void At_least_one_amount_is_required()
    {
        Assert.ThrowsException<DomainException>(() => Create(null, null, null, null));
    }

    [TestMethod]
    public void One_sided_age_and_sale_ranges_are_supported()
    {
        var price = new FlightPrice(1, 10, 20, new DateOnly(2027, 1, 1),
            new DateOnly(2027, 12, 31), (byte)PriceDaysOfWeek.All,
            new DateOnly(2026, 11, 1), null, 12, null, 100m, null, null, 0, "user", Now);

        Assert.AreEqual((short?)12, price.AgeFrom);
        Assert.IsNull(price.AgeTo);
        Assert.IsNull(price.SaleDateTo);
    }

    private static FlightPrice Create(short? ageFrom, short? ageTo, decimal? netto, decimal? brutto) =>
        new(1, 10, 20, new DateOnly(2027, 1, 1), new DateOnly(2027, 12, 31),
            (byte)PriceDaysOfWeek.All, null, null, ageFrom, ageTo, netto, brutto,
            null, 0, "user", Now);
}
