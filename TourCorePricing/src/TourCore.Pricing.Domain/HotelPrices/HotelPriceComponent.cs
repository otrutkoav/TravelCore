using TourCore.Pricing.Domain.Common;

namespace TourCore.Pricing.Domain.HotelPrices;

public sealed class HotelPriceComponent : AuditableEntity
{
    public int HotelPriceRuleId { get; private set; }
    public HotelPriceComponentType Type { get; private set; }
    public PriceGuestType? GuestType { get; private set; }
    public PricePlacementType PlacementType { get; private set; }
    public short? AgeFrom { get; private set; }
    public short? AgeTo { get; private set; }
    public PriceOperation Operation { get; private set; }
    public PriceApplicationUnit ApplicationUnit { get; private set; }
    public decimal? NettoAmount { get; private set; }
    public decimal? BruttoAmount { get; private set; }
    public decimal? Percent { get; private set; }
    public PercentageBase? PercentageBase { get; private set; }
    public string CreatedByUserId { get; private set; } = null!;

    private HotelPriceComponent() { }

    public HotelPriceComponent(int hotelPriceRuleId, HotelPriceComponentType type,
        PriceGuestType? guestType, PricePlacementType placementType, short? ageFrom, short? ageTo,
        PriceOperation operation, PriceApplicationUnit applicationUnit, decimal? netAmount,
        decimal? grossAmount, decimal? percent, PercentageBase? percentageBase,
        string createdByUserId, DateTime createdAt)
    {
        SetValues(hotelPriceRuleId, type, guestType, placementType, ageFrom, ageTo,
            operation, applicationUnit, netAmount, grossAmount, percent, percentageBase);
        CreatedByUserId = NormalizeUser(createdByUserId);
        SetCreated(createdAt);
    }

    public void Update(HotelPriceComponentType type, PriceGuestType? guestType,
        PricePlacementType placementType, short? ageFrom, short? ageTo, PriceOperation operation,
        PriceApplicationUnit applicationUnit, decimal? netAmount, decimal? grossAmount,
        decimal? percent, PercentageBase? percentageBase, DateTime updatedAt)
    {
        SetValues(HotelPriceRuleId, type, guestType, placementType, ageFrom, ageTo,
            operation, applicationUnit, netAmount, grossAmount, percent, percentageBase);
        SetUpdated(updatedAt);
    }

    private void SetValues(int hotelPriceRuleId, HotelPriceComponentType type,
        PriceGuestType? guestType, PricePlacementType placementType, short? ageFrom, short? ageTo,
        PriceOperation operation, PriceApplicationUnit applicationUnit, decimal? netAmount,
        decimal? grossAmount, decimal? percent, PercentageBase? percentageBase)
    {
        HotelPriceRuleId = HotelPriceRule.Positive(hotelPriceRuleId, nameof(HotelPriceRuleId));
        if (!Enum.IsDefined(type) || guestType.HasValue && !Enum.IsDefined(guestType.Value)
            || !Enum.IsDefined(placementType) || !Enum.IsDefined(operation)
            || !Enum.IsDefined(applicationUnit) || percentageBase.HasValue && !Enum.IsDefined(percentageBase.Value))
            throw new DomainException("Unknown hotel price component enum value.");
        if (ageFrom is < 0 || ageTo is < 0 || ageFrom > ageTo)
            throw new DomainException("Component age range is invalid.");
        HotelPriceRule.ValidateMoney(netAmount, nameof(NettoAmount));
        HotelPriceRule.ValidateMoney(grossAmount, nameof(BruttoAmount));
        var percentageOperation = operation is PriceOperation.AddPercent or PriceOperation.SubtractPercent;
        if (percentageOperation)
        {
            if (percent is null or <= 0 || percent > 1000 || !percentageBase.HasValue)
                throw new DomainException("Percentage operation requires percent and percentage base.");
            if (netAmount.HasValue || grossAmount.HasValue)
                throw new DomainException("Percentage component cannot contain money amounts.");
        }
        else
        {
            if (!netAmount.HasValue && !grossAmount.HasValue)
                throw new DomainException("Amount operation requires a net or gross amount.");
            if (percent.HasValue || percentageBase.HasValue)
                throw new DomainException("Amount component cannot contain percentage values.");
        }

        Type = type;
        GuestType = guestType;
        PlacementType = placementType;
        AgeFrom = ageFrom;
        AgeTo = ageTo;
        Operation = operation;
        ApplicationUnit = applicationUnit;
        NettoAmount = netAmount;
        BruttoAmount = grossAmount;
        Percent = percent;
        PercentageBase = percentageBase;
    }

    private static string NormalizeUser(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new DomainException("CreatedByUserId is required.");
        value = value.Trim();
        if (value.Length > HotelPriceRule.MaximumUserIdLength) throw new DomainException("CreatedByUserId is too long.");
        return value;
    }
}
