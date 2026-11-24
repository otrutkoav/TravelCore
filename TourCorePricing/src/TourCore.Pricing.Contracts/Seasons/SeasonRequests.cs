namespace TourCore.Pricing.Contracts.Seasons;

/// <summary>Общие изменяемые данные сезона.</summary>
public abstract class SeasonWriteRequest
{
    /// <summary>Название сезона на русском языке.</summary>
    public string Name { get; init; } = null!;
    /// <summary>Латинское или международное название сезона.</summary>
    public string NameLat { get; init; } = null!;
    /// <summary>Можно ли выбирать сезон для новых ценовых блоков.</summary>
    public bool IsActive { get; init; }
    /// <summary>Основной сезон, используемый по умолчанию.</summary>
    public bool IsMain { get; init; }
}

/// <summary>Запрос создания сезона.</summary>
public sealed class CreateSeasonRequest : SeasonWriteRequest;

/// <summary>Запрос изменения сезона.</summary>
public sealed class UpdateSeasonRequest : SeasonWriteRequest;
