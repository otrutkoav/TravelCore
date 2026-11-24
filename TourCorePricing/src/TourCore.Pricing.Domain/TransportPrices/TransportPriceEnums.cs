namespace TourCore.Pricing.Domain.TransportPrices;

/// <summary>К кому относится указанная сумма.</summary>
public enum TransportPriceScope : byte
{
    PerPassenger = 1,
    PerService = 2
}

/// <summary>Как сумма применяется по продолжительности переезда.</summary>
public enum TransportPricePeriodUnit : byte
{
    PerTrip = 1,
    PerDay = 2
}
