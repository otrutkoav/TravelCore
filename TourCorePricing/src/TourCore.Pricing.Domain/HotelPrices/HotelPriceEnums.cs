namespace TourCore.Pricing.Domain.HotelPrices;

[Flags]
public enum PriceDaysOfWeek : byte
{
    Monday = 1,
    Tuesday = 2,
    Wednesday = 4,
    Thursday = 8,
    Friday = 16,
    Saturday = 32,
    Sunday = 64,
    All = Monday | Tuesday | Wednesday | Thursday | Friday | Saturday | Sunday
}

public enum HotelPriceModel : byte
{
    FixedAccommodation = 1,
    PerPerson = 2,
    BaseOccupancy = 3,
    WholeUnit = 4
}

public enum AgeCalculationDate : byte
{
    CheckIn = 1,
    CheckOut = 2
}

public enum PriceGuestType : byte
{
    Adult = 1,
    Child = 2,
    Infant = 3
}

public enum PricePlacementType : byte
{
    Any = 0,
    MainPlace = 1,
    ExtraPlace = 2,
    WithoutBed = 3
}

public enum PriceOperation : byte
{
    SetAmount = 1,
    AddAmount = 2,
    SubtractAmount = 3,
    AddPercent = 4,
    SubtractPercent = 5
}

public enum PriceApplicationUnit : byte
{
    PerStay = 1,
    PerNight = 2,
    PerRoomPerStay = 3,
    PerRoomPerNight = 4,
    PerPersonPerStay = 5,
    PerPersonPerNight = 6
}

public enum HotelPriceComponentType : byte
{
    GuestPrice = 1,
    GuestSupplement = 2,
    GuestDiscount = 3,
    SingleUseSupplement = 4,
    ExtraBedSupplement = 5,
    InfantPrice = 6
}

public enum PercentageBase : byte
{
    AdultMainPlacePrice = 1,
    GuestBasePrice = 2,
    RoomBasePrice = 3,
    TotalBeforeComponent = 4
}
