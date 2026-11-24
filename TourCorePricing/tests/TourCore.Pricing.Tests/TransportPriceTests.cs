using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Pricing.Domain.BusPrices;
using TourCore.Pricing.Domain.Common;
using TourCore.Pricing.Domain.HotelPrices;
using TourCore.Pricing.Domain.RailwayPrices;
using TourCore.Pricing.Domain.TransportPrices;

namespace TourCore.Pricing.Tests;

[TestClass]
public sealed class TransportPriceTests
{
    private static readonly DateTime Now = new(2026, 11, 24, 13, 0, 0, DateTimeKind.Utc);

    [TestMethod]
    public void Bus_price_supports_whole_service_and_passenger_count_range()
    {
        var price = new BusTransferPrice(1, 2, 3, null, null, (byte)PriceDaysOfWeek.All,
            null, null, null, null, 1, 20, TransportPriceScope.PerService,
            TransportPricePeriodUnit.PerTrip, 100m, 120m, null, 0, "user", Now);

        Assert.AreEqual(TransportPriceScope.PerService, price.Scope);
        Assert.AreEqual((short?)20, price.PassengerCountTo);
    }

    [TestMethod]
    public void Railway_price_has_no_seating_plan_identifier()
    {
        var price = new RailwayTransferPrice(1, 2, 3, null, null,
            (byte)PriceDaysOfWeek.All, null, null, 12, 99,
            TransportPriceScope.PerPassenger, TransportPricePeriodUnit.PerTrip,
            100m, null, null, 0, "user", Now);

        Assert.AreEqual(2, price.RailwayTransferId);
        Assert.AreEqual(3, price.TransportId);
    }

    [TestMethod]
    public void Bus_passenger_range_must_be_valid()
    {
        Assert.ThrowsException<DomainException>(() => new BusTransferPrice(
            1, 2, 3, null, null, (byte)PriceDaysOfWeek.All, null, null, null, null,
            20, 1, TransportPriceScope.PerPassenger, TransportPricePeriodUnit.PerTrip,
            100m, null, null, 0, "user", Now));
    }
}
