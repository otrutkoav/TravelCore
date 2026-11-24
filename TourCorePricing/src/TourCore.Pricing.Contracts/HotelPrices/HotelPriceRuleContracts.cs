namespace TourCore.Pricing.Contracts.HotelPrices;

/// <summary>Изменяемые данные правила стоимости проживания.</summary>
public abstract class HotelPriceRuleWriteRequest
{
    /// <summary>Идентификатор ценового блока.</summary>
    public int PriceBlockId { get; init; }
    /// <summary>Идентификатор отеля из Catalog.</summary>
    public int HotelId { get; init; }
    /// <summary>Идентификатор типа номера из Catalog.</summary>
    public int RoomTypeId { get; init; }
    /// <summary>Идентификатор категории номера из Catalog.</summary>
    public int RoomCategoryId { get; init; }
    /// <summary>Идентификатор типа размещения. Null — правило подходит для любого размещения.</summary>
    public int? AccommodationTypeId { get; init; }
    /// <summary>Идентификатор включённого типа питания. Null — питание правилом не определяется.</summary>
    public int? MealTypeId { get; init; }
    /// <summary>Модель цены: 0 — готовая цена, 1 — формула из компонентов.</summary>
    public byte Model { get; init; }
    /// <summary>Приоритет при пересечении правил: большее значение побеждает.</summary>
    public int Priority { get; init; }
    /// <summary>Первая дата проживания, для которой действует правило.</summary>
    public DateOnly StayDateFrom { get; init; }
    /// <summary>Последняя дата проживания, для которой действует правило.</summary>
    public DateOnly StayDateTo { get; init; }
    /// <summary>Допустимая дата заезда от. Null — без нижней границы.</summary>
    public DateOnly? CheckInDateFrom { get; init; }
    /// <summary>Допустимая дата заезда до. Null — без верхней границы.</summary>
    public DateOnly? CheckInDateTo { get; init; }
    /// <summary>Маска дней недели проживания: понедельник = 1, ... воскресенье = 64; 127 — все дни.</summary>
    public byte DaysOfWeekMask { get; init; } = 127;
    /// <summary>Минимальная продолжительность проживания в ночах.</summary>
    public short? MinNights { get; init; }
    /// <summary>Максимальная продолжительность проживания в ночах.</summary>
    public short? MaxNights { get; init; }
    /// <summary>Дата определения возраста ребёнка: 0 — дата заезда, 1 — дата выезда.</summary>
    public byte AgeCalculationDate { get; init; }
    /// <summary>Единица применения базовой суммы: 0 — за проживание, 1 — за ночь.</summary>
    public byte ApplicationUnit { get; init; }
    /// <summary>Нетто-сумма готовой цены. Для формульной модели сумма задаётся компонентами.</summary>
    public decimal? NettoAmount { get; init; }
    /// <summary>Брутто-сумма готовой цены. Для формульной модели сумма задаётся компонентами.</summary>
    public decimal? BruttoAmount { get; init; }
    /// <summary>Валюта правила из Catalog. Null означает валюту ценового блока.</summary>
    public int? CurrencyId { get; init; }
}

public sealed class CreateHotelPriceRuleRequest : HotelPriceRuleWriteRequest;
public sealed class UpdateHotelPriceRuleRequest : HotelPriceRuleWriteRequest;

/// <summary>Правило стоимости проживания.</summary>
public sealed class HotelPriceRuleDto
{
    /// <summary>Идентификатор правила.</summary>
    public int Id { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.PriceBlockId"/>
    public int PriceBlockId { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.HotelId"/>
    public int HotelId { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.RoomTypeId"/>
    public int RoomTypeId { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.RoomCategoryId"/>
    public int RoomCategoryId { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.AccommodationTypeId"/>
    public int? AccommodationTypeId { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.MealTypeId"/>
    public int? MealTypeId { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.Model"/>
    public byte Model { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.Priority"/>
    public int Priority { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.StayDateFrom"/>
    public DateOnly StayDateFrom { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.StayDateTo"/>
    public DateOnly StayDateTo { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.CheckInDateFrom"/>
    public DateOnly? CheckInDateFrom { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.CheckInDateTo"/>
    public DateOnly? CheckInDateTo { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.DaysOfWeekMask"/>
    public byte DaysOfWeekMask { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.MinNights"/>
    public short? MinNights { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.MaxNights"/>
    public short? MaxNights { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.AgeCalculationDate"/>
    public byte AgeCalculationDate { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.ApplicationUnit"/>
    public byte ApplicationUnit { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.NettoAmount"/>
    public decimal? NettoAmount { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.BruttoAmount"/>
    public decimal? BruttoAmount { get; init; }
    /// <inheritdoc cref="HotelPriceRuleWriteRequest.CurrencyId"/>
    public int? CurrencyId { get; init; }
    /// <summary>Идентификатор пользователя, создавшего правило.</summary>
    public string CreatedByUserId { get; init; } = null!;
    /// <summary>Дата и время создания в UTC.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Дата и время последнего изменения в UTC.</summary>
    public DateTime? UpdatedAt { get; init; }
}
