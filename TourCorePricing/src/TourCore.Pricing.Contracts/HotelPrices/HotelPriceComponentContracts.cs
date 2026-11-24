namespace TourCore.Pricing.Contracts.HotelPrices;

/// <summary>Изменяемые данные компонента гостиничной цены.</summary>
public abstract class HotelPriceComponentWriteRequest
{
    /// <summary>Тип компонента: 0 — база за номер, 1 — место гостя, 2 — минимальная стоимость номера.</summary>
    public byte Type { get; init; }
    /// <summary>Тип гостя: 0 — взрослый, 1 — ребёнок, 2 — младенец. Null — любой.</summary>
    public byte? GuestType { get; init; }
    /// <summary>Тип места: 0 — любое, 1 — основное, 2 — дополнительное, 3 — без места.</summary>
    public byte PlacementType { get; init; }
    /// <summary>Минимальный возраст гостя включительно.</summary>
    public short? AgeFrom { get; init; }
    /// <summary>Максимальный возраст гостя включительно.</summary>
    public short? AgeTo { get; init; }
    /// <summary>Операция: 0 — установить сумму, 1 — добавить сумму, 2 — вычесть сумму, 3 — применить процент.</summary>
    public byte Operation { get; init; }
    /// <summary>Единица применения: 0 — за проживание, 1 — за ночь.</summary>
    public byte ApplicationUnit { get; init; }
    /// <summary>Нетто-сумма компонента.</summary>
    public decimal? NettoAmount { get; init; }
    /// <summary>Брутто-сумма компонента.</summary>
    public decimal? BruttoAmount { get; init; }
    /// <summary>Процент для процентной операции.</summary>
    public decimal? Percent { get; init; }
    /// <summary>База процента: 0 — базовая цена, 1 — накопленная цена.</summary>
    public byte? PercentageBase { get; init; }
}

public sealed class CreateHotelPriceComponentRequest : HotelPriceComponentWriteRequest;
public sealed class UpdateHotelPriceComponentRequest : HotelPriceComponentWriteRequest;

/// <summary>Компонент расчёта гостиничной цены.</summary>
public sealed class HotelPriceComponentDto
{
    /// <summary>Идентификатор компонента.</summary>
    public int Id { get; init; }
    /// <summary>Идентификатор родительского правила проживания.</summary>
    public int HotelPriceRuleId { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.Type"/>
    public byte Type { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.GuestType"/>
    public byte? GuestType { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.PlacementType"/>
    public byte PlacementType { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.AgeFrom"/>
    public short? AgeFrom { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.AgeTo"/>
    public short? AgeTo { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.Operation"/>
    public byte Operation { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.ApplicationUnit"/>
    public byte ApplicationUnit { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.NettoAmount"/>
    public decimal? NettoAmount { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.BruttoAmount"/>
    public decimal? BruttoAmount { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.Percent"/>
    public decimal? Percent { get; init; }
    /// <inheritdoc cref="HotelPriceComponentWriteRequest.PercentageBase"/>
    public byte? PercentageBase { get; init; }
    /// <summary>Идентификатор пользователя, создавшего компонент.</summary>
    public string CreatedByUserId { get; init; } = null!;
    /// <summary>Дата и время создания в UTC.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Дата и время последнего изменения в UTC.</summary>
    public DateTime? UpdatedAt { get; init; }
}
