namespace TourCore.Pricing.Contracts.PriceBlocks;

/// <summary>Общие изменяемые данные ценового блока.</summary>
public abstract class PriceBlockWriteRequest
{
    /// <summary>Идентификатор родительского ценового пакета.</summary>
    public int PricePackageId { get; init; }
    /// <summary>Идентификатор типа SPO.</summary>
    public int SPOTypeId { get; init; }
    /// <summary>Идентификатор сезона.</summary>
    public int SeasonId { get; init; }
    /// <summary>Идентификатор типа услуги из Catalog: отель, авиаперелёт, автобус и т. п.</summary>
    public int ServiceTypeId { get; init; }
    /// <summary>Идентификатор партнёра — поставщика цен.</summary>
    public int PartnerId { get; init; }
    /// <summary>Основная валюта блока из Catalog. Отдельная цена может переопределить её.</summary>
    public int CurrencyId { get; init; }
    /// <summary>Название ценового блока.</summary>
    public string Name { get; init; } = null!;
    /// <summary>Внешний или пользовательский код блока.</summary>
    public string? Code { get; init; }
    /// <summary>Описание назначения и условий блока.</summary>
    public string? Description { get; init; }
    /// <summary>Внутренний комментарий.</summary>
    public string? Comment { get; init; }
    /// <summary>Текст, предназначенный для показа в онлайне.</summary>
    public string? OnlineInfoText { get; init; }
    /// <summary>Дата и время начала продаж. Null — без ограничения снизу.</summary>
    public DateTime? SaleStartsAt { get; init; }
    /// <summary>Дата и время окончания продаж. Null — без ограничения сверху.</summary>
    public DateTime? SaleEndsAt { get; init; }
    /// <summary>Минимальное число дней от продажи до начала услуги.</summary>
    public int? DaysBeforeCheckInFrom { get; init; }
    /// <summary>Максимальное число дней от продажи до начала услуги.</summary>
    public int? DaysBeforeCheckInTo { get; init; }
    /// <summary>Не использовать цены блока при массовом перерасчёте.</summary>
    public bool ExcludeFromRecalculation { get; init; }
}

/// <summary>Запрос создания ценового блока.</summary>
public sealed class CreatePriceBlockRequest : PriceBlockWriteRequest;

/// <summary>Запрос изменения ценового блока.</summary>
public sealed class UpdatePriceBlockRequest : PriceBlockWriteRequest;
