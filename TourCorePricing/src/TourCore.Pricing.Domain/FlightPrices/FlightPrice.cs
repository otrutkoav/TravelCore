using TourCore.Pricing.Domain.Common;

namespace TourCore.Pricing.Domain.FlightPrices;

/// <summary>Цена одного пассажира на один сегмент перелёта.</summary>
public sealed class FlightPrice : AuditableEntity
{
    public const int MaximumUserIdLength = 200;

    public int PriceBlockId { get; private set; }
    public int CharterId { get; private set; }
    public int AirClassId { get; private set; }
    public DateOnly TravelDateFrom { get; private set; }
    public DateOnly TravelDateTo { get; private set; }
    public byte DaysOfWeekMask { get; private set; }
    public DateOnly? SaleDateFrom { get; private set; }
    public DateOnly? SaleDateTo { get; private set; }
    public short? AgeFrom { get; private set; }
    public short? AgeTo { get; private set; }
    public decimal? NettoAmount { get; private set; }
    public decimal? BruttoAmount { get; private set; }
    public int? CurrencyId { get; private set; }
    public int Priority { get; private set; }
    public string CreatedByUserId { get; private set; } = null!;

    private FlightPrice() { }

    public FlightPrice(int priceBlockId, int charterId, int airClassId,
        DateOnly travelDateFrom, DateOnly travelDateTo, byte daysOfWeekMask,
        DateOnly? saleDateFrom, DateOnly? saleDateTo, short? ageFrom, short? ageTo,
        decimal? nettoAmount, decimal? bruttoAmount, int? currencyId, int priority,
        string createdByUserId, DateTime createdAt)
    {
        SetValues(priceBlockId, charterId, airClassId, travelDateFrom, travelDateTo,
            daysOfWeekMask, saleDateFrom, saleDateTo, ageFrom, ageTo, nettoAmount,
            bruttoAmount, currencyId, priority);
        CreatedByUserId = NormalizeUser(createdByUserId);
        SetCreated(createdAt);
    }

    public void Update(int priceBlockId, int charterId, int airClassId,
        DateOnly travelDateFrom, DateOnly travelDateTo, byte daysOfWeekMask,
        DateOnly? saleDateFrom, DateOnly? saleDateTo, short? ageFrom, short? ageTo,
        decimal? nettoAmount, decimal? bruttoAmount, int? currencyId, int priority,
        DateTime updatedAt)
    {
        SetValues(priceBlockId, charterId, airClassId, travelDateFrom, travelDateTo,
            daysOfWeekMask, saleDateFrom, saleDateTo, ageFrom, ageTo, nettoAmount,
            bruttoAmount, currencyId, priority);
        SetUpdated(updatedAt);
    }

    private void SetValues(int priceBlockId, int charterId, int airClassId,
        DateOnly travelDateFrom, DateOnly travelDateTo, byte daysOfWeekMask,
        DateOnly? saleDateFrom, DateOnly? saleDateTo, short? ageFrom, short? ageTo,
        decimal? nettoAmount, decimal? bruttoAmount, int? currencyId, int priority)
    {
        if (priceBlockId <= 0 || charterId <= 0 || airClassId <= 0)
            throw new DomainException("Price block, charter and air class identifiers must be greater than zero.");
        if (travelDateFrom > travelDateTo)
            throw new DomainException("Travel date from cannot be later than travel date to.");
        if (saleDateFrom > saleDateTo)
            throw new DomainException("Sale date from cannot be later than sale date to.");
        if (daysOfWeekMask is < 1 or > 127)
            throw new DomainException("Days of week mask must contain at least one valid day.");
        if (ageFrom is < 0 || ageTo is < 0 || ageFrom > ageTo)
            throw new DomainException("Passenger age range is invalid.");
        if (nettoAmount is < 0 || bruttoAmount is < 0)
            throw new DomainException("Flight price amounts cannot be negative.");
        if (!nettoAmount.HasValue && !bruttoAmount.HasValue)
            throw new DomainException("Netto or brutto amount is required.");
        if (currencyId is <= 0) throw new DomainException("Currency id must be greater than zero when specified.");
        if (priority < 0) throw new DomainException("Priority cannot be negative.");

        PriceBlockId = priceBlockId;
        CharterId = charterId;
        AirClassId = airClassId;
        TravelDateFrom = travelDateFrom;
        TravelDateTo = travelDateTo;
        DaysOfWeekMask = daysOfWeekMask;
        SaleDateFrom = saleDateFrom;
        SaleDateTo = saleDateTo;
        AgeFrom = ageFrom;
        AgeTo = ageTo;
        NettoAmount = nettoAmount;
        BruttoAmount = bruttoAmount;
        CurrencyId = currencyId;
        Priority = priority;
    }

    private static string NormalizeUser(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new DomainException("CreatedByUserId is required.");
        value = value.Trim();
        if (value.Length > MaximumUserIdLength) throw new DomainException("CreatedByUserId is too long.");
        return value;
    }
}
