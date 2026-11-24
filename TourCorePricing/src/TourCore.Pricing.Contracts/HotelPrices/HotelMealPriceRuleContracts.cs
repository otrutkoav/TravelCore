namespace TourCore.Pricing.Contracts.HotelPrices;

/// <summary>Изменяемые данные правила стоимости питания или апгрейда питания.</summary>
public abstract class HotelMealPriceRuleWriteRequest
{
    /// <summary>Идентификатор ценового блока.</summary>
    public int PriceBlockId { get; init; }
    /// <summary>Идентификатор отеля из Catalog.</summary>
    public int HotelId { get; init; }
    /// <summary>Ограничение по типу номера. Null — любой тип.</summary>
    public int? RoomTypeId { get; init; }
    /// <summary>Ограничение по категории номера. Null — любая категория.</summary>
    public int? RoomCategoryId { get; init; }
    /// <summary>Ограничение по типу размещения. Null — любое размещение.</summary>
    public int? AccommodationTypeId { get; init; }
    /// <summary>Исходный тип питания для апгрейда. Null означает отдельную стоимость питания без перехода.</summary>
    public int? FromMealTypeId { get; init; }
    /// <summary>Тип питания, стоимость которого задаёт правило.</summary>
    public int ToMealTypeId { get; init; }
    /// <summary>Первая дата проживания, для которой действует правило.</summary>
    public DateOnly StayDateFrom { get; init; }
    /// <summary>Последняя дата проживания, для которой действует правило.</summary>
    public DateOnly StayDateTo { get; init; }
    /// <summary>Допустимая дата заезда от.</summary>
    public DateOnly? CheckInDateFrom { get; init; }
    /// <summary>Допустимая дата заезда до.</summary>
    public DateOnly? CheckInDateTo { get; init; }
    /// <summary>Маска дней недели: понедельник = 1, ... воскресенье = 64; 127 — все дни.</summary>
    public byte DaysOfWeekMask { get; init; } = 127;
    /// <summary>Тип гостя: 0 — взрослый, 1 — ребёнок, 2 — младенец. Null — любой.</summary>
    public byte? GuestType { get; init; }
    /// <summary>Тип места: 0 — любое, 1 — основное, 2 — дополнительное, 3 — без места.</summary>
    public byte PlacementType { get; init; }
    /// <summary>Минимальный возраст гостя включительно.</summary>
    public short? AgeFrom { get; init; }
    /// <summary>Максимальный возраст гостя включительно.</summary>
    public short? AgeTo { get; init; }
    /// <summary>Единица применения: 0 — за проживание, 1 — за ночь.</summary>
    public byte ApplicationUnit { get; init; }
    /// <summary>Операция: 0 — установить сумму, 1 — добавить, 2 — вычесть, 3 — применить процент.</summary>
    public byte Operation { get; init; }
    /// <summary>Нетто-сумма питания или апгрейда.</summary>
    public decimal? NettoAmount { get; init; }
    /// <summary>Брутто-сумма питания или апгрейда.</summary>
    public decimal? BruttoAmount { get; init; }
    /// <summary>Процент для процентной операции.</summary>
    public decimal? Percent { get; init; }
    /// <summary>База процента: 0 — базовая цена, 1 — накопленная цена.</summary>
    public byte? PercentageBase { get; init; }
    /// <summary>Валюта из Catalog. Null означает валюту ценового блока.</summary>
    public int? CurrencyId { get; init; }
    /// <summary>Приоритет при пересечении правил: большее значение побеждает.</summary>
    public int Priority { get; init; }
}

public sealed class CreateHotelMealPriceRuleRequest : HotelMealPriceRuleWriteRequest;
public sealed class UpdateHotelMealPriceRuleRequest : HotelMealPriceRuleWriteRequest;

/// <summary>Правило стоимости питания или перехода между типами питания.</summary>
public sealed class HotelMealPriceRuleDto
{
    /// <summary>Идентификатор правила.</summary>
    public int Id { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.PriceBlockId"/>
    public int PriceBlockId { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.HotelId"/>
    public int HotelId { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.RoomTypeId"/>
    public int? RoomTypeId { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.RoomCategoryId"/>
    public int? RoomCategoryId { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.AccommodationTypeId"/>
    public int? AccommodationTypeId { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.FromMealTypeId"/>
    public int? FromMealTypeId { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.ToMealTypeId"/>
    public int ToMealTypeId { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.StayDateFrom"/>
    public DateOnly StayDateFrom { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.StayDateTo"/>
    public DateOnly StayDateTo { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.CheckInDateFrom"/>
    public DateOnly? CheckInDateFrom { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.CheckInDateTo"/>
    public DateOnly? CheckInDateTo { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.DaysOfWeekMask"/>
    public byte DaysOfWeekMask { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.GuestType"/>
    public byte? GuestType { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.PlacementType"/>
    public byte PlacementType { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.AgeFrom"/>
    public short? AgeFrom { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.AgeTo"/>
    public short? AgeTo { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.ApplicationUnit"/>
    public byte ApplicationUnit { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.Operation"/>
    public byte Operation { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.NettoAmount"/>
    public decimal? NettoAmount { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.BruttoAmount"/>
    public decimal? BruttoAmount { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.Percent"/>
    public decimal? Percent { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.PercentageBase"/>
    public byte? PercentageBase { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.CurrencyId"/>
    public int? CurrencyId { get; init; }
    /// <inheritdoc cref="HotelMealPriceRuleWriteRequest.Priority"/>
    public int Priority { get; init; }
    /// <summary>Идентификатор пользователя, создавшего правило.</summary>
    public string CreatedByUserId { get; init; } = null!;
    /// <summary>Дата и время создания в UTC.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Дата и время последнего изменения в UTC.</summary>
    public DateTime? UpdatedAt { get; init; }
}
