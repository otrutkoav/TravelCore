using TourCore.Pricing.Domain.Common;

namespace TourCore.Pricing.Domain.TransportPrices;

internal static class TransportPriceValidation
{
    public static void Validate(int priceBlockId, int serviceId, int transportId,
        DateOnly? travelDateFrom, DateOnly? travelDateTo, byte daysOfWeekMask,
        DateOnly? saleDateFrom, DateOnly? saleDateTo, short? ageFrom, short? ageTo,
        TransportPriceScope scope, TransportPricePeriodUnit periodUnit,
        decimal? nettoAmount, decimal? bruttoAmount, int? currencyId, int priority)
    {
        if (priceBlockId <= 0 || serviceId <= 0 || transportId <= 0)
            throw new DomainException("Price block, service and transport identifiers must be greater than zero.");
        if (travelDateFrom > travelDateTo || saleDateFrom > saleDateTo)
            throw new DomainException("Price date range is invalid.");
        if (daysOfWeekMask is < 1 or > 127) throw new DomainException("Days of week mask is invalid.");
        if (ageFrom is < 0 || ageTo is < 0 || ageFrom > ageTo) throw new DomainException("Age range is invalid.");
        if (!Enum.IsDefined(scope) || !Enum.IsDefined(periodUnit)) throw new DomainException("Unknown transport price unit.");
        if (nettoAmount is < 0 || bruttoAmount is < 0 || !nettoAmount.HasValue && !bruttoAmount.HasValue)
            throw new DomainException("A non-negative netto or brutto amount is required.");
        if (currencyId is <= 0) throw new DomainException("Currency id must be positive when specified.");
        if (priority < 0) throw new DomainException("Priority cannot be negative.");
    }

    public static string NormalizeUser(string value, int maximumLength)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new DomainException("CreatedByUserId is required.");
        value = value.Trim();
        if (value.Length > maximumLength) throw new DomainException("CreatedByUserId is too long.");
        return value;
    }
}
