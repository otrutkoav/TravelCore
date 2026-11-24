namespace TourCore.Pricing.Contracts.Seasons;

/// <summary>Общие изменяемые данные сезона.</summary>
public abstract class SeasonWriteRequest
{
    public string Name { get; init; } = null!;
    public string NameLat { get; init; } = null!;
    public bool IsActive { get; init; }
    public bool IsMain { get; init; }
}

/// <summary>Запрос создания сезона.</summary>
public sealed class CreateSeasonRequest : SeasonWriteRequest;

/// <summary>Запрос изменения сезона.</summary>
public sealed class UpdateSeasonRequest : SeasonWriteRequest;
