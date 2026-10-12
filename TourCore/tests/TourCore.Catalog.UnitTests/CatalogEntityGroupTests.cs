using Microsoft.VisualStudio.TestTools.UnitTesting;
using TourCore.Domain.Avia.Entities;
using TourCore.Domain.Bus.Entities;
using TourCore.Domain.Common.Exceptions;
using TourCore.Domain.Finance.Entities;
using TourCore.Domain.Hotels.Entities;
using TourCore.Domain.Railway.Entities;
using TourCore.Domain.Seating.Entities;
using TourCore.Domain.Services.Entities;
using TourCore.Domain.Transfers.Entities;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Catalog.UnitTests;

[TestClass]
public sealed class CatalogEntityGroupTests
{
    private static readonly DateTime UtcNow = new(2026, 10, 12, 10, 0, 0, DateTimeKind.Utc);

    [TestMethod]
    public void Avia_normalizes_codes_and_rejects_negative_sort_order()
    {
        var airClass = new AirClass(" y ", " Economy ", UtcNow, sortOrder: 1);

        Assert.AreEqual("Y", airClass.Code);
        Assert.AreEqual("Economy", airClass.Name);
        Assert.ThrowsException<DomainException>(() =>
            new AirClass("Y", "Economy", UtcNow, sortOrder: -1));
    }

    [TestMethod]
    public void Hotels_normalize_global_code_and_reject_negative_print_order()
    {
        var category = new HotelCategory(" Five stars ", UtcNow, globalCode: " 5s ");

        Assert.AreEqual("Five stars", category.Name);
        Assert.AreEqual("5S", category.GlobalCode);
        Assert.ThrowsException<DomainException>(() =>
            new HotelCategory("Invalid", UtcNow, printOrder: -1));
    }

    [TestMethod]
    public void Finance_normalizes_iso_code_and_requires_rate_name()
    {
        var rate = new Rate("RUB", " Ruble ", UtcNow, " rub ", isMain: true);

        Assert.AreEqual("Ruble", rate.Name);
        Assert.AreEqual("RUB", rate.IsoCode);
        Assert.IsTrue(rate.IsMain);
        Assert.ThrowsException<DomainException>(() => new Rate("RUB", " ", UtcNow));
    }

    [TestMethod]
    public void Bus_requires_valid_route_identifiers()
    {
        var transfer = new BusTransfer(" Route ", 1, 2, 3, 4, UtcNow);

        Assert.AreEqual("Route", transfer.Name);
        Assert.ThrowsException<DomainException>(() =>
            new BusTransfer("Route", 1, 0, 3, 4, UtcNow));
    }

    [TestMethod]
    public void Railway_requires_valid_route_identifiers()
    {
        var transfer = new RailwayTransfer(" Train route ", 1, 2, 3, 4, UtcNow);

        Assert.AreEqual("Train route", transfer.Name);
        Assert.ThrowsException<DomainException>(() =>
            new RailwayTransfer("Route", 1, 2, 3, 0, UtcNow));
    }

    [TestMethod]
    public void Transportation_rejects_negative_capacity()
    {
        var transport = new Transport(" Coach ", UtcNow, seatsCount: 50, showOrder: 2);

        Assert.AreEqual("Coach", transport.Name);
        Assert.AreEqual((short)50, transport.SeatsCount);
        Assert.ThrowsException<DomainException>(() =>
            new Transport("Coach", UtcNow, seatsCount: -1));
    }

    [TestMethod]
    public void Seating_requires_positive_dimensions_and_transport()
    {
        var plan = new VehiclePlan(1, 10, 4, 1, UtcNow, " Main ");

        Assert.AreEqual("Main", plan.Name);
        Assert.AreEqual(40, plan.RowsCount * plan.ColumnsCount);
        Assert.ThrowsException<DomainException>(() =>
            new VehiclePlan(1, 0, 4, 1, UtcNow));
    }

    [TestMethod]
    public void Services_normalize_code_and_preserve_behavior_flags()
    {
        var serviceType = new ServiceType(
            " Transfer ", UtcNow, " bus ", isDuration: true, isRoute: true);

        Assert.AreEqual("Transfer", serviceType.Name);
        Assert.AreEqual("BUS", serviceType.Code);
        Assert.IsTrue(serviceType.IsDuration);
        Assert.IsTrue(serviceType.IsRoute);
    }

    [TestMethod]
    public void Transfers_require_and_trim_direction_name()
    {
        var direction = new TransferDirection(" Airport to hotel ", UtcNow);

        Assert.AreEqual("Airport to hotel", direction.Name);
        Assert.ThrowsException<DomainException>(() => new TransferDirection(" ", UtcNow));
    }
}
