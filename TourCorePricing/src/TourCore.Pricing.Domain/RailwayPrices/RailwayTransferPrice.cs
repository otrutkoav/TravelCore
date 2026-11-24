using TourCore.Pricing.Domain.Common;
using TourCore.Pricing.Domain.TransportPrices;

namespace TourCore.Pricing.Domain.RailwayPrices;

public sealed class RailwayTransferPrice : AuditableEntity
{
    public const int MaximumUserIdLength = 200;
    public int PriceBlockId { get; private set; }
    public int RailwayTransferId { get; private set; }
    public int TransportId { get; private set; }
    public DateOnly? TravelDateFrom { get; private set; }
    public DateOnly? TravelDateTo { get; private set; }
    public byte DaysOfWeekMask { get; private set; }
    public DateOnly? SaleDateFrom { get; private set; }
    public DateOnly? SaleDateTo { get; private set; }
    public short? AgeFrom { get; private set; }
    public short? AgeTo { get; private set; }
    public TransportPriceScope Scope { get; private set; }
    public TransportPricePeriodUnit PeriodUnit { get; private set; }
    public decimal? NettoAmount { get; private set; }
    public decimal? BruttoAmount { get; private set; }
    public int? CurrencyId { get; private set; }
    public int Priority { get; private set; }
    public string CreatedByUserId { get; private set; } = null!;

    private RailwayTransferPrice() { }

    public RailwayTransferPrice(int priceBlockId, int railwayTransferId, int transportId,
        DateOnly? travelDateFrom, DateOnly? travelDateTo, byte daysOfWeekMask,
        DateOnly? saleDateFrom, DateOnly? saleDateTo, short? ageFrom, short? ageTo,
        TransportPriceScope scope, TransportPricePeriodUnit periodUnit,
        decimal? nettoAmount, decimal? bruttoAmount, int? currencyId, int priority,
        string createdByUserId, DateTime createdAt)
    {
        SetValues(priceBlockId, railwayTransferId, transportId, travelDateFrom, travelDateTo,
            daysOfWeekMask, saleDateFrom, saleDateTo, ageFrom, ageTo, scope, periodUnit,
            nettoAmount, bruttoAmount, currencyId, priority);
        CreatedByUserId = TransportPriceValidation.NormalizeUser(createdByUserId, MaximumUserIdLength);
        SetCreated(createdAt);
    }

    public void Update(int priceBlockId, int railwayTransferId, int transportId,
        DateOnly? travelDateFrom, DateOnly? travelDateTo, byte daysOfWeekMask,
        DateOnly? saleDateFrom, DateOnly? saleDateTo, short? ageFrom, short? ageTo,
        TransportPriceScope scope, TransportPricePeriodUnit periodUnit,
        decimal? nettoAmount, decimal? bruttoAmount, int? currencyId, int priority,
        DateTime updatedAt)
    {
        SetValues(priceBlockId, railwayTransferId, transportId, travelDateFrom, travelDateTo,
            daysOfWeekMask, saleDateFrom, saleDateTo, ageFrom, ageTo, scope, periodUnit,
            nettoAmount, bruttoAmount, currencyId, priority);
        SetUpdated(updatedAt);
    }

    private void SetValues(int priceBlockId, int railwayTransferId, int transportId,
        DateOnly? travelDateFrom, DateOnly? travelDateTo, byte daysOfWeekMask,
        DateOnly? saleDateFrom, DateOnly? saleDateTo, short? ageFrom, short? ageTo,
        TransportPriceScope scope, TransportPricePeriodUnit periodUnit,
        decimal? nettoAmount, decimal? bruttoAmount, int? currencyId, int priority)
    {
        TransportPriceValidation.Validate(priceBlockId, railwayTransferId, transportId,
            travelDateFrom, travelDateTo, daysOfWeekMask, saleDateFrom, saleDateTo,
            ageFrom, ageTo, scope, periodUnit, nettoAmount, bruttoAmount, currencyId, priority);
        PriceBlockId = priceBlockId;
        RailwayTransferId = railwayTransferId;
        TransportId = transportId;
        TravelDateFrom = travelDateFrom;
        TravelDateTo = travelDateTo;
        DaysOfWeekMask = daysOfWeekMask;
        SaleDateFrom = saleDateFrom;
        SaleDateTo = saleDateTo;
        AgeFrom = ageFrom;
        AgeTo = ageTo;
        Scope = scope;
        PeriodUnit = periodUnit;
        NettoAmount = nettoAmount;
        BruttoAmount = bruttoAmount;
        CurrencyId = currencyId;
        Priority = priority;
    }
}
