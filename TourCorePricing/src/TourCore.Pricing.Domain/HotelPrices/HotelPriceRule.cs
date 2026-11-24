using TourCore.Pricing.Domain.Common;

namespace TourCore.Pricing.Domain.HotelPrices;

public sealed class HotelPriceRule : AuditableEntity
{
    public const int MaximumUserIdLength = 200;

    public int PriceBlockId { get; private set; }
    public int HotelId { get; private set; }
    public int RoomTypeId { get; private set; }
    public int RoomCategoryId { get; private set; }
    public int? AccommodationTypeId { get; private set; }
    public int? MealTypeId { get; private set; }
    public HotelPriceModel Model { get; private set; }
    public int Priority { get; private set; }
    public DateOnly StayDateFrom { get; private set; }
    public DateOnly StayDateTo { get; private set; }
    public DateOnly? CheckInDateFrom { get; private set; }
    public DateOnly? CheckInDateTo { get; private set; }
    public byte DaysOfWeekMask { get; private set; }
    public short? MinNights { get; private set; }
    public short? MaxNights { get; private set; }
    public AgeCalculationDate AgeCalculationDate { get; private set; }
    public PriceApplicationUnit ApplicationUnit { get; private set; }
    public decimal? NettoAmount { get; private set; }
    public decimal? BruttoAmount { get; private set; }
    public int? CurrencyId { get; private set; }
    public string CreatedByUserId { get; private set; } = null!;

    private HotelPriceRule() { }

    public HotelPriceRule(
        int priceBlockId, int hotelId, int roomTypeId, int roomCategoryId,
        int? accommodationTypeId, int? mealTypeId, HotelPriceModel model, int priority,
        DateOnly stayDateFrom, DateOnly stayDateTo, DateOnly? checkInDateFrom,
        DateOnly? checkInDateTo, byte daysOfWeekMask, short? minNights, short? maxNights,
        AgeCalculationDate ageCalculationDate, PriceApplicationUnit applicationUnit,
        decimal? netAmount, decimal? grossAmount, int? currencyId,
        string createdByUserId, DateTime createdAt)
    {
        SetValues(priceBlockId, hotelId, roomTypeId, roomCategoryId, accommodationTypeId,
            mealTypeId, model, priority, stayDateFrom, stayDateTo, checkInDateFrom,
            checkInDateTo, daysOfWeekMask, minNights, maxNights, ageCalculationDate,
            applicationUnit, netAmount, grossAmount, currencyId);
        CreatedByUserId = RequiredUser(createdByUserId);
        SetCreated(createdAt);
    }

    public void Update(
        int priceBlockId, int hotelId, int roomTypeId, int roomCategoryId,
        int? accommodationTypeId, int? mealTypeId, HotelPriceModel model, int priority,
        DateOnly stayDateFrom, DateOnly stayDateTo, DateOnly? checkInDateFrom,
        DateOnly? checkInDateTo, byte daysOfWeekMask, short? minNights, short? maxNights,
        AgeCalculationDate ageCalculationDate, PriceApplicationUnit applicationUnit,
        decimal? netAmount, decimal? grossAmount, int? currencyId, DateTime updatedAt)
    {
        SetValues(priceBlockId, hotelId, roomTypeId, roomCategoryId, accommodationTypeId,
            mealTypeId, model, priority, stayDateFrom, stayDateTo, checkInDateFrom,
            checkInDateTo, daysOfWeekMask, minNights, maxNights, ageCalculationDate,
            applicationUnit, netAmount, grossAmount, currencyId);
        SetUpdated(updatedAt);
    }

    private void SetValues(
        int priceBlockId, int hotelId, int roomTypeId, int roomCategoryId,
        int? accommodationTypeId, int? mealTypeId, HotelPriceModel model, int priority,
        DateOnly stayDateFrom, DateOnly stayDateTo, DateOnly? checkInDateFrom,
        DateOnly? checkInDateTo, byte daysOfWeekMask, short? minNights, short? maxNights,
        AgeCalculationDate ageCalculationDate, PriceApplicationUnit applicationUnit,
        decimal? netAmount, decimal? grossAmount, int? currencyId)
    {
        PriceBlockId = Positive(priceBlockId, nameof(PriceBlockId));
        HotelId = Positive(hotelId, nameof(HotelId));
        RoomTypeId = Positive(roomTypeId, nameof(RoomTypeId));
        RoomCategoryId = Positive(roomCategoryId, nameof(RoomCategoryId));
        AccommodationTypeId = OptionalPositive(accommodationTypeId, nameof(AccommodationTypeId));
        MealTypeId = OptionalPositive(mealTypeId, nameof(MealTypeId));
        CurrencyId = OptionalPositive(currencyId, nameof(CurrencyId));
        if (!Enum.IsDefined(model)) throw new DomainException("Unknown hotel price model.");
        if (!Enum.IsDefined(ageCalculationDate)) throw new DomainException("Unknown age calculation date.");
        if (!Enum.IsDefined(applicationUnit)) throw new DomainException("Unknown price application unit.");
        if (priority < 0) throw new DomainException("Priority cannot be negative.");
        if (stayDateFrom > stayDateTo) throw new DomainException("Stay date from cannot be later than stay date to.");
        if (checkInDateFrom.HasValue != checkInDateTo.HasValue)
            throw new DomainException("Both check-in period boundaries must be specified together.");
        if (checkInDateFrom > checkInDateTo)
            throw new DomainException("Check-in date from cannot be later than check-in date to.");
        if (daysOfWeekMask is < 1 or > 127)
            throw new DomainException("Days of week mask must contain at least one valid day.");
        if (minNights is <= 0 || maxNights is <= 0)
            throw new DomainException("Night limits must be greater than zero.");
        if (minNights > maxNights) throw new DomainException("Minimum nights cannot exceed maximum nights.");
        ValidateMoney(netAmount, nameof(NettoAmount));
        ValidateMoney(grossAmount, nameof(BruttoAmount));
        if (model == HotelPriceModel.FixedAccommodation && !accommodationTypeId.HasValue)
            throw new DomainException("Fixed accommodation price requires an accommodation type.");
        if (model is HotelPriceModel.FixedAccommodation or HotelPriceModel.BaseOccupancy or HotelPriceModel.WholeUnit
            && !netAmount.HasValue && !grossAmount.HasValue)
            throw new DomainException("This hotel price model requires a base net or gross amount.");

        Model = model;
        Priority = priority;
        StayDateFrom = stayDateFrom;
        StayDateTo = stayDateTo;
        CheckInDateFrom = checkInDateFrom;
        CheckInDateTo = checkInDateTo;
        DaysOfWeekMask = daysOfWeekMask;
        MinNights = minNights;
        MaxNights = maxNights;
        AgeCalculationDate = ageCalculationDate;
        ApplicationUnit = applicationUnit;
        NettoAmount = netAmount;
        BruttoAmount = grossAmount;
    }

    internal static void ValidateMoney(decimal? value, string name)
    {
        if (value is < 0) throw new DomainException($"{name} cannot be negative.");
    }

    internal static int Positive(int value, string name)
    {
        if (value <= 0) throw new DomainException($"{name} must be greater than zero.");
        return value;
    }

    internal static int? OptionalPositive(int? value, string name)
    {
        if (value is <= 0) throw new DomainException($"{name} must be greater than zero when specified.");
        return value;
    }

    private static string RequiredUser(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new DomainException("CreatedByUserId is required.");
        value = value.Trim();
        if (value.Length > MaximumUserIdLength) throw new DomainException("CreatedByUserId is too long.");
        return value;
    }
}
