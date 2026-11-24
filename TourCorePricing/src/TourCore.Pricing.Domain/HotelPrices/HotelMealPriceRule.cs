using TourCore.Pricing.Domain.Common;

namespace TourCore.Pricing.Domain.HotelPrices;

public sealed class HotelMealPriceRule : AuditableEntity
{
    public int PriceBlockId { get; private set; }
    public int HotelId { get; private set; }
    public int? RoomTypeId { get; private set; }
    public int? RoomCategoryId { get; private set; }
    public int? AccommodationTypeId { get; private set; }
    public int? FromMealTypeId { get; private set; }
    public int ToMealTypeId { get; private set; }
    public DateOnly StayDateFrom { get; private set; }
    public DateOnly StayDateTo { get; private set; }
    public DateOnly? CheckInDateFrom { get; private set; }
    public DateOnly? CheckInDateTo { get; private set; }
    public byte DaysOfWeekMask { get; private set; }
    public PriceGuestType? GuestType { get; private set; }
    public PricePlacementType PlacementType { get; private set; }
    public short? AgeFrom { get; private set; }
    public short? AgeTo { get; private set; }
    public PriceApplicationUnit ApplicationUnit { get; private set; }
    public PriceOperation Operation { get; private set; }
    public decimal? NettoAmount { get; private set; }
    public decimal? BruttoAmount { get; private set; }
    public decimal? Percent { get; private set; }
    public PercentageBase? PercentageBase { get; private set; }
    public int? CurrencyId { get; private set; }
    public int Priority { get; private set; }
    public string CreatedByUserId { get; private set; } = null!;

    private HotelMealPriceRule() { }

    public HotelMealPriceRule(int priceBlockId, int hotelId, int? roomTypeId,
        int? roomCategoryId, int? accommodationTypeId, int? fromMealTypeId, int toMealTypeId,
        DateOnly stayDateFrom, DateOnly stayDateTo, DateOnly? checkInDateFrom,
        DateOnly? checkInDateTo, byte daysOfWeekMask, PriceGuestType? guestType,
        PricePlacementType placementType, short? ageFrom, short? ageTo,
        PriceApplicationUnit applicationUnit, PriceOperation operation, decimal? netAmount,
        decimal? grossAmount, decimal? percent, PercentageBase? percentageBase,
        int? currencyId, int priority, string createdByUserId, DateTime createdAt)
    {
        SetValues(priceBlockId, hotelId, roomTypeId, roomCategoryId, accommodationTypeId,
            fromMealTypeId, toMealTypeId, stayDateFrom, stayDateTo, checkInDateFrom,
            checkInDateTo, daysOfWeekMask, guestType, placementType, ageFrom, ageTo,
            applicationUnit, operation, netAmount, grossAmount, percent, percentageBase,
            currencyId, priority);
        CreatedByUserId = NormalizeUser(createdByUserId);
        SetCreated(createdAt);
    }

    public void Update(int priceBlockId, int hotelId, int? roomTypeId,
        int? roomCategoryId, int? accommodationTypeId, int? fromMealTypeId, int toMealTypeId,
        DateOnly stayDateFrom, DateOnly stayDateTo, DateOnly? checkInDateFrom,
        DateOnly? checkInDateTo, byte daysOfWeekMask, PriceGuestType? guestType,
        PricePlacementType placementType, short? ageFrom, short? ageTo,
        PriceApplicationUnit applicationUnit, PriceOperation operation, decimal? netAmount,
        decimal? grossAmount, decimal? percent, PercentageBase? percentageBase,
        int? currencyId, int priority, DateTime updatedAt)
    {
        SetValues(priceBlockId, hotelId, roomTypeId, roomCategoryId, accommodationTypeId,
            fromMealTypeId, toMealTypeId, stayDateFrom, stayDateTo, checkInDateFrom,
            checkInDateTo, daysOfWeekMask, guestType, placementType, ageFrom, ageTo,
            applicationUnit, operation, netAmount, grossAmount, percent, percentageBase,
            currencyId, priority);
        SetUpdated(updatedAt);
    }

    private void SetValues(int priceBlockId, int hotelId, int? roomTypeId,
        int? roomCategoryId, int? accommodationTypeId, int? fromMealTypeId, int toMealTypeId,
        DateOnly stayDateFrom, DateOnly stayDateTo, DateOnly? checkInDateFrom,
        DateOnly? checkInDateTo, byte daysOfWeekMask, PriceGuestType? guestType,
        PricePlacementType placementType, short? ageFrom, short? ageTo,
        PriceApplicationUnit applicationUnit, PriceOperation operation, decimal? netAmount,
        decimal? grossAmount, decimal? percent, PercentageBase? percentageBase,
        int? currencyId, int priority)
    {
        PriceBlockId = HotelPriceRule.Positive(priceBlockId, nameof(PriceBlockId));
        HotelId = HotelPriceRule.Positive(hotelId, nameof(HotelId));
        RoomTypeId = HotelPriceRule.OptionalPositive(roomTypeId, nameof(RoomTypeId));
        RoomCategoryId = HotelPriceRule.OptionalPositive(roomCategoryId, nameof(RoomCategoryId));
        AccommodationTypeId = HotelPriceRule.OptionalPositive(accommodationTypeId, nameof(AccommodationTypeId));
        FromMealTypeId = HotelPriceRule.OptionalPositive(fromMealTypeId, nameof(FromMealTypeId));
        ToMealTypeId = HotelPriceRule.Positive(toMealTypeId, nameof(ToMealTypeId));
        CurrencyId = HotelPriceRule.OptionalPositive(currencyId, nameof(CurrencyId));
        if (fromMealTypeId == toMealTypeId) throw new DomainException("Source and target meal types must differ.");
        if (stayDateFrom > stayDateTo) throw new DomainException("Stay date range is invalid.");
        if (checkInDateFrom.HasValue != checkInDateTo.HasValue || checkInDateFrom > checkInDateTo)
            throw new DomainException("Check-in date range is invalid.");
        if (daysOfWeekMask is < 1 or > 127) throw new DomainException("Days of week mask is invalid.");
        if (priority < 0) throw new DomainException("Priority cannot be negative.");
        if (guestType.HasValue && !Enum.IsDefined(guestType.Value) || !Enum.IsDefined(placementType)
            || !Enum.IsDefined(applicationUnit) || !Enum.IsDefined(operation)
            || percentageBase.HasValue && !Enum.IsDefined(percentageBase.Value))
            throw new DomainException("Unknown meal price enum value.");
        if (ageFrom is < 0 || ageTo is < 0 || ageFrom > ageTo) throw new DomainException("Age range is invalid.");
        HotelPriceRule.ValidateMoney(netAmount, nameof(NettoAmount));
        HotelPriceRule.ValidateMoney(grossAmount, nameof(BruttoAmount));
        var percentageOperation = operation is PriceOperation.AddPercent or PriceOperation.SubtractPercent;
        if (percentageOperation)
        {
            if (percent is null or <= 0 || percent > 1000 || !percentageBase.HasValue)
                throw new DomainException("Percentage meal rule requires percent and percentage base.");
            if (netAmount.HasValue || grossAmount.HasValue)
                throw new DomainException("Percentage meal rule cannot contain amounts.");
        }
        else
        {
            if (!netAmount.HasValue && !grossAmount.HasValue)
                throw new DomainException("Amount meal rule requires a net or gross amount.");
            if (percent.HasValue || percentageBase.HasValue)
                throw new DomainException("Amount meal rule cannot contain percentage values.");
        }

        PriceBlockId = priceBlockId;
        HotelId = hotelId;
        RoomTypeId = roomTypeId;
        RoomCategoryId = roomCategoryId;
        AccommodationTypeId = accommodationTypeId;
        FromMealTypeId = fromMealTypeId;
        ToMealTypeId = toMealTypeId;
        StayDateFrom = stayDateFrom;
        StayDateTo = stayDateTo;
        CheckInDateFrom = checkInDateFrom;
        CheckInDateTo = checkInDateTo;
        DaysOfWeekMask = daysOfWeekMask;
        GuestType = guestType;
        PlacementType = placementType;
        AgeFrom = ageFrom;
        AgeTo = ageTo;
        ApplicationUnit = applicationUnit;
        Operation = operation;
        NettoAmount = netAmount;
        BruttoAmount = grossAmount;
        Percent = percent;
        PercentageBase = percentageBase;
        CurrencyId = currencyId;
        Priority = priority;
    }

    private static string NormalizeUser(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new DomainException("CreatedByUserId is required.");
        value = value.Trim();
        if (value.Length > HotelPriceRule.MaximumUserIdLength) throw new DomainException("CreatedByUserId is too long.");
        return value;
    }
}
