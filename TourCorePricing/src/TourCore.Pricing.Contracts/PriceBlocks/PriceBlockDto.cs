namespace TourCore.Pricing.Contracts.PriceBlocks;

/// <summary>Ценовой блок.</summary>
public sealed class PriceBlockDto
{
    /// <summary>Идентификатор блока.</summary>
    public int Id { get; init; }
    /// <summary>Идентификатор родительского ценового пакета.</summary>
    public int PricePackageId { get; init; }
    /// <summary>Идентификатор типа SPO.</summary>
    public int SPOTypeId { get; init; }
    /// <summary>Идентификатор сезона.</summary>
    public int SeasonId { get; init; }
    /// <summary>Идентификатор типа услуги из Catalog.</summary>
    public int ServiceTypeId { get; init; }
    /// <summary>Идентификатор партнёра — поставщика цен.</summary>
    public int PartnerId { get; init; }
    /// <summary>Основная валюта блока из Catalog.</summary>
    public int CurrencyId { get; init; }
    /// <summary>Название блока.</summary>
    public string Name { get; init; } = null!;
    /// <summary>Внешний или пользовательский код блока.</summary>
    public string Code { get; init; } = string.Empty;
    /// <summary>Описание назначения и условий блока.</summary>
    public string Description { get; init; } = string.Empty;
    /// <summary>Внутренний комментарий.</summary>
    public string? Comment { get; init; }
    /// <summary>Текст для показа в онлайне.</summary>
    public string? OnlineInfoText { get; init; }
    /// <summary>Начало периода продаж.</summary>
    public DateTime? SaleStartsAt { get; init; }
    /// <summary>Окончание периода продаж.</summary>
    public DateTime? SaleEndsAt { get; init; }
    /// <summary>Минимум дней от продажи до начала услуги.</summary>
    public int? DaysBeforeCheckInFrom { get; init; }
    /// <summary>Максимум дней от продажи до начала услуги.</summary>
    public int? DaysBeforeCheckInTo { get; init; }
    /// <summary>Исключён ли блок из массового перерасчёта.</summary>
    public bool ExcludeFromRecalculation { get; init; }
    /// <summary>Состояние: 0 — новый, 1 — на проверке, 2 — активирован для нетто, 3 — активирован, 4 — деактивирован.</summary>
    public byte State { get; init; }
    /// <summary>Дата и время создания в UTC.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Дата и время последнего изменения в UTC.</summary>
    public DateTime? UpdatedAt { get; init; }
    /// <summary>Дата и время отправки на проверку.</summary>
    public DateTime? SentForReviewAt { get; init; }
    /// <summary>Дата и время активации для расчётов нетто.</summary>
    public DateTime? ActivatedForNetAt { get; init; }
    /// <summary>Дата и время полной активации.</summary>
    public DateTime? ActivatedAt { get; init; }
    /// <summary>Дата и время деактивации.</summary>
    public DateTime? DeactivatedAt { get; init; }
    /// <summary>Пользователь, создавший блок.</summary>
    public string CreatedByUserId { get; init; } = null!;
    /// <summary>Пользователь, отправивший блок на проверку.</summary>
    public string? SentForReviewByUserId { get; init; }
    /// <summary>Пользователь, активировавший блок для нетто.</summary>
    public string? ActivatedForNetByUserId { get; init; }
    /// <summary>Пользователь, полностью активировавший блок.</summary>
    public string? ActivatedByUserId { get; init; }
    /// <summary>Пользователь, деактивировавший блок.</summary>
    public string? DeactivatedByUserId { get; init; }
}
